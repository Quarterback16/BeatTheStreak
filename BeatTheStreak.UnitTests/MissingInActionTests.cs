using Application;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using BeatTheStreak.UnitTests.Fakes;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace BeatTheStreak.UnitTests
{
	[TestClass]
	public class MissingInActionTests
	{
		private MissingInAction _sut;
		private Mock<ILineupRepository> _mockLineupRepository;
		private Mock<IPlayerStatsRepository> _mockPlayerStatsRepository;
		private Mock<IPickerOptions> _mockPickerOptions;

		[TestInitialize]
		public void SetUp()
		{
			_mockLineupRepository = new Mock<ILineupRepository>();
			_mockPlayerStatsRepository = new Mock<IPlayerStatsRepository>();
			_mockPickerOptions = new Mock<IPickerOptions>();
			_mockPickerOptions
				.Setup(x => x.OptionOn(It.IsAny<string>()))
				.Returns(true);
			_mockPickerOptions
				.Setup(x => x.IntegerOption(Constants.Options.DaysOffDaysBack))
				.Returns(3);
			_mockPickerOptions
				.Setup(x => x.IntegerOption(Constants.Options.HotBattersDaysBack))
				.Returns(30);
			_mockPickerOptions
				.Setup(x => x.DecimalOption(It.IsAny<string>()))
				.Returns(.299M);
			_sut = new MissingInAction(
				_mockLineupRepository.Object,
				_mockPlayerStatsRepository.Object,
				_mockPickerOptions.Object);
		}

		[TestMethod]
		public void MissingInAction_WhenBatterHasNotThreeApps_Rejects()
		{
			_mockLineupRepository
				.Setup(x => x.Submit(It.IsAny<DateTime>(), It.IsAny<string>()))
				.Returns(
					new LineupViewModel
					{
						Lineup = new List<Batter>()
					});  // all lineup request will say no appearance
			string reasonForDislike = string.Empty;
			var result = _sut.Likes(
				new Selection
				{
					GameDate = new DateTime(2018, 5, 1),
					Batter = new FakeBatter(),
					Batter1 = new FakeBatter(.300M),
					Batter2 = new FakeBatter(.250M),
					Batter3 = new FakeBatter(.275M)
				},
				out reasonForDislike);
			Assert.IsFalse(
				result,
				"Picker should return false as batter has not appeared in last 3 games");
		}

		[TestMethod]

		public void MissingInAction_WhenBatterHasThreeApps_Accepts()
		{
			_mockPlayerStatsRepository
				.Setup(
					x => x.Submit(
						new DateTime(2018, 05, 31),
						"mlb-test-player"))
				.Returns(
					new PlayerStatsViewModel
					{
						AsOf = new System.DateTime(2018, 05, 31),
						AtBats = 100,
						Hits = 31
					});
			_mockPlayerStatsRepository
				.Setup(
					x => x.Submit(
						new DateTime(2018, 05, 01),
						"mlb-test-player"))
				.Returns(
					new PlayerStatsViewModel
					{
						AsOf = new DateTime(2018, 05, 01),
						AtBats = 50,
						Hits = 15
					});
			var selection = new Selection
			{
				GameDate = new DateTime(2018, 06, 01),
				Batter = new FakeBatter()
			};
			_mockLineupRepository
				.Setup(x => x.Submit(new DateTime(2018, 5, 31), It.IsAny<string>()))
				.Returns(new LineupViewModel
				{
					Lineup = new List<Batter>
					{
							  new FakeBatter()
					}
				});
			_mockLineupRepository
				.Setup(x => x.Submit(new DateTime(2018, 5, 30), It.IsAny<string>()))
				.Returns(new LineupViewModel
				{
					Lineup = new List<Batter>
					{
							  new FakeBatter()
					}
				});
			_mockLineupRepository
				.Setup(x => x.Submit(new DateTime(2018, 5, 29), It.IsAny<string>()))
				.Returns(new LineupViewModel
				{
					Lineup = new List<Batter>
					{
							  new FakeBatter()
					}
				});
			string reasonForDislike = string.Empty;
			var result = _sut.Likes(
				new Selection
				{
					GameDate = new DateTime(2018, 6, 1),
					Batter = new FakeBatter(),
					Batter1 = new FakeBatter(.300M),
					Batter2 = new FakeBatter(.250M),
					Batter3 = new FakeBatter(.275M)
				},
				out reasonForDislike);
			Assert.IsTrue(
				result,
				"Picker should return true as batter has appeared in last 3 days");
		}
	}
}
