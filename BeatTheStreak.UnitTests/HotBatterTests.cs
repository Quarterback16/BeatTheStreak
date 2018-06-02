using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Domain;
using Application;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;

namespace BeatTheStreak.UnitTests
{
	[TestClass]
	public class HotBatterTests
	{
		private HotBatter _sut;
		private Mock<IPickerOptions> _mockPickerOptions;

		[TestInitialize]
		public void SetUp()
		{
			_mockPickerOptions = new Mock<IPickerOptions>();
			_mockPickerOptions
				.Setup(x => x.OptionOn(It.IsAny<string>()))
				.Returns(true);
			_mockPickerOptions
				.Setup(x => x.DecimalOption(It.IsAny<string>()))
				.Returns(.299M);
			_sut = new HotBatter(
				_mockPickerOptions.Object);
		}

		[TestMethod]
		public void HotBatter_Likes_300Hitter()
		{
			var selection = new Selection
			{
				GameDate = new System.DateTime(2018,06,01),
				Batter = new Batter
				{
					BattingAverage = .321M,
					PlayerSlug = "mlb-test-player"
				}
			};
			var reason = string.Empty;
			var result = _sut.Likes(selection, out reason);
			Assert.IsTrue(result);
		}

		[TestMethod]
		public void HotBatter_DisLikes_200Hitter()
		{
			var selection = new Selection
			{
				GameDate = new System.DateTime(2018, 06, 01),
				Batter = new Batter
				{
					BattingAverage = .201M,
					PlayerSlug = "mlb-test-player"
				}
			};
			var reason = string.Empty;
			var result = _sut.Likes(selection, out reason);
			Assert.IsFalse(result,"Should not like such a low batter");
		}
	}
}
