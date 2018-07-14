using Application;
using BeatTheStreak.Implementations;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Repositories;
using BeatTheStreak.Tests.Fakes;
using Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class CalculateStreakIntegrationTests
	{
		private CalculateStreak _sut;

		private IPicker _picker;

		private IResultChecker _resultChecker;

		[TestInitialize]
		public void Setup()
		{
			var cache = new RedisCacheRepository(
				connectionString: "localhost,abortConnect=false",
				environment: "local",
				functionalArea: "bts",
				serializer: new XmlSerializer(),
				logger: new FakeCacheLogger(),
				expire: false);
			var pitcherRepo = new CachedPitcherRepository(
				new PitcherRepository(),
				cache);
			var lineupRepo = new CachedLineupRepository(
				new LineupRepository(),
				cache);
			var statsRepo = new CachedPlayerStatsRepository(
				new PlayerStatsRepository(),
				cache);
			var opposingPitcher = new OpposingPitcher(
				pitcherRepo);
			var lineupProjector = new LineupProjector(
				lineupRepo,
				opposingPitcher,
				new FakeLogger(),
				daysToGoBack: 10);
			var gameLogRepository = new CachedGameLogRepository(
				new GameLogRepository(),
				cache);
			var obaCalculator = new CalculateOpponentOba(
				new FakeLogger(),
				gameLogRepository);
			_resultChecker = new ResultChecker(statsRepo);
			var options = new Dictionary<string, string>
			{
				{ Constants.Options.HomePitchersOnly, "on" },
				{ Constants.Options.NoDaysOff, "off" },
				{ Constants.Options.DaysOffDaysBack, "3" },
				{ Constants.Options.HotBatters, "on" },
				{ Constants.Options.HotBattersDaysBack, "35" },
				{ Constants.Options.HotBattersMendozaLine, ".299" },
				{ Constants.Options.PitchersMendozaLine, ".259" },
				{ Constants.Options.PitcherDaysBack, "35" },
			};
			var pickerOptions = new PickerOptions(options);
			_picker = new DefaultPicker(
				pickerOptions,
				lineupRepo,
				pitcherRepo,
				statsRepo,
				lineupProjector,
				obaCalculator,
				new FakeLogger());

			_sut = new CalculateStreak(_picker, _resultChecker);
		}

		[TestMethod]
		public void CalculateStreak_Instantiates_Ok()
		{
			Assert.IsNotNull(_sut);
		}

		[TestMethod]
		public void CalculateStreak_ForDay1_ReturnsResult()
		{
			var result = _sut.StreakFor(
				new DateTime(2018, 3, 29),
				new DateTime(2018, 3, 29));
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void CalculateStreak_ForDay1_ReturnsOneGameDay()
		{
			var result = _sut.StreakFor(
				new DateTime(2018, 3, 29),
				new DateTime(2018, 3, 29));
			Assert.IsNotNull(result.GameDays);
			Assert.AreEqual(1, result.GameDays.Count);
		}

		[TestMethod]
		public void CalculateStreak_ForDay1_ReturnsNoSelections()
		{
			var result = _sut.StreakFor(
				new DateTime(2018, 3, 29),
				new DateTime(2018, 3, 29));
			Assert.IsNotNull(result.GameDays);
			Assert.AreEqual(
				expected: 0,
				actual: result.GameDays[0].Selections.Count,
				message: "Cant select on first day all Pitcher stats are zeroised");
		}

		[TestMethod]
		public void CalculateStreak_ForDay2_ReturnsNoSelections()
		{
			var result = _sut.StreakFor(
				new DateTime(2018, 3, 30),
				new DateTime(2018, 3, 30));
			Assert.IsNotNull(result.GameDays);
			Assert.AreEqual(
				expected: 0,
				actual: result.GameDays[0].Selections.Count,
				message: "Cant select on second day all Pitcher stats are zeroised");
		}

		[TestMethod]
		public void CalculateStreak_ForDay7_ReturnsTwoSelections()
		{
			var result = _sut.StreakFor(
				new DateTime(2018, 4, 3),
				new DateTime(2018, 4, 3));
			result.Dump();
			Assert.IsNotNull(result.GameDays);
			Assert.AreEqual(
				expected: 2,
				actual: result.GameDays[0].Selections.Count,
				message: "Can select on seventh day");
		}

		[TestMethod]
		public void CalculateStreak_ForDay8_DefaultScoresZero()
		{
			var result = _sut.StreakFor(
				new DateTime(2018, 4, 3),
				new DateTime(2018, 4, 4));
			result.Dump();

			Assert.AreEqual(
				expected: 0,
				actual: result.BestStreak,
				message: "Score after 2 days was zero");
		}

		[TestMethod]
		public void CalculateStreak_ForDay9_DefaultScoresTwo()
		{
			var result = _sut.StreakFor(
				new DateTime(2018, 4, 3),
				new DateTime(2018, 4, 5));
			result.Dump();

			Assert.AreEqual(
				expected: 2,
				actual: result.BestStreak,
				message: "Score after 3 days was two");
		}

		[TestMethod]
		public void CalculateStreak_ForDayMany_DefaultScoresTen()
		{
			var result = _sut.StreakFor(
				new DateTime(2018, 4, 3),
				new DateTime(2018, 7, 7));
			result.Dump();

			Assert.AreEqual(
				expected: 10,
				actual: result.BestStreak,
				message: "Score after many days was ten");
		}

	}
}
