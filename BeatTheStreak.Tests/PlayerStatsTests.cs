using BeatTheStreak.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeatTheStreak.Tests
{
    [TestClass]
    public class PlayerStatsTests
    {
        [TestMethod]
        public void PlayerStatsRequest_PitcherOk()
        {
			var playerSlug = "mlb-jameson-taillon";

			var sut = new PlayerStatsRequest();
            var result = sut.Submit(
                queryDate: new DateTime(2019, 3, 30),
                playerSlug: playerSlug );
            result.DumpPitcher();
		}

        [TestMethod]
        public void PlayerStatsRequest_BatterOk()
        {
			//  last data was as of sob 2019-06-20
			var playerSlug = "mlb-christian-yelich";
			var querySobDate = new DateTime(2019, 6, 21);
			//var actualBattingAverage = 0.274M;
			var sut = new PlayerStatsRequest();
            var result = sut.Submit(
                queryDate: querySobDate,
                playerSlug: playerSlug);
            result.Dump();
            //Assert.IsTrue(
            //    result.BattingAverage.Equals(actualBattingAverage),
            //    $"Joc Pedersons Season Bavg on {queryDate:u} was {actualBattingAverage} not {result.BattingAverage}");
		}

        [TestMethod]
        public void PlayerStatsRequestOneDay_BatterOk()
        {
            var sut = new GameLogRequest();
            var result = sut.Submit(
                queryDate: new DateTime(2019, 4, 7),
                playerSlug: "mlb-joc-pederson");
			if (result.IsSuccess)
			{
				result.Value.BatterLine();
				Assert.IsTrue(
					result.Value.BattingAverage.Equals(0.250M),
					"Josh Bells Bavg on 2018-05-11 was .250 1 for 4");
			}
        }

		[TestMethod]
		public void PlayerStatsRequestOneDay_PitcherOk()
		{
			var sut = new GameLogRequest();
			var result = sut.Submit(
				queryDate: new DateTime(2018, 6, 27),
				playerSlug: "mlb-ivan-nova");
			if (result.IsSuccess)
			{
				result.Value.DumpPitcher();
				Assert.AreEqual(
					4.5M,
					result.Value.Era,
					"Ivan Novas ERA on 2018-06-27 was 4.5 for the game");
			}
		}

		[TestMethod]
		public void PlayerStatsRequestOneDay_NoPitcherDay()
		{
			var sut = new GameLogRequest();
			var result = sut.Submit(
				queryDate: new DateTime(2018, 6, 26),
				playerSlug: "mlb-ivan-nova");
			if (result.IsSuccess)
			{
				result.Value.DumpPitcher();
				Assert.AreEqual(
					0,
					result.Value.InningsPitched,
					"Ivan Novas ERA on 2018-06-27 was 4.5 for the game");
			}
		}

		[TestMethod]
		public void DailyRankingRequest_Batters()
		{
			var playerSlug = "mlb-christian-yelich";

			var sut = new DailyRankingRequest();
			var result = sut.Submit(
				queryDate: new DateTime(2019, 6, 1),
				playerSlug: playerSlug);
			result.Dump();
			//  just gets an empty json structure
		}
	}
}
