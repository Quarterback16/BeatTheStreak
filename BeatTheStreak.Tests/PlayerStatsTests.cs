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
                queryDate: new DateTime(2018, 5, 1),
                playerSlug: playerSlug );
            result.DumpPitcher();
			var result2 = sut.Submit(
				queryDate: new DateTime(2018, 5, 31),
				playerSlug: playerSlug);
			result2.DumpPitcher();
		}

        [TestMethod]
        public void PlayerStatsRequest_BatterOk()
        {
			var playerSlug = "mlb-josh-bell";

			var sut = new PlayerStatsRequest();
            var result = sut.Submit(
                queryDate: new DateTime(2018, 5, 4),
                playerSlug: playerSlug);
            result.Dump();
            Assert.IsTrue(
                result.BattingAverage.Equals(0.244M),
                "Josh Bells Season Bavg on 2018-05-04 was .244");
			var result2 = sut.Submit(
				queryDate: new DateTime(2018, 5, 31),
				playerSlug: playerSlug);
			result2.Dump();
		}

        [TestMethod]
        public void PlayerStatsRequestOneDay_BatterOk()
        {
            var sut = new GameLogRequest();
            var result = sut.Submit(
                queryDate: new DateTime(2018, 5, 30),
                playerSlug: "mlb-josh-bell");
            result.BatterLine();
            Assert.IsTrue(
                result.BattingAverage.Equals(0.250M),
                "Josh Bells Bavg on 2018-05-11 was .250 1 for 4");
        }

		[TestMethod]
		public void PlayerStatsRequestOneDay_PitcherOk()
		{
			var sut = new GameLogRequest();
			var result = sut.Submit(
				queryDate: new DateTime(2018, 6, 27),
				playerSlug: "mlb-ivan-nova");
			result.DumpPitcher();
			Assert.AreEqual(
				4.5M,
				result.Era,
				"Ivan Novas ERA on 2018-06-27 was 4.5 for the game");
		}

		[TestMethod]
		public void PlayerStatsRequestOneDay_NoPitcherDay()
		{
			var sut = new GameLogRequest();
			var result = sut.Submit(
				queryDate: new DateTime(2018, 6, 26),
				playerSlug: "mlb-ivan-nova");
			result.DumpPitcher();
			Assert.AreEqual(
				0,
				result.InningsPitched,
				"Ivan Novas ERA on 2018-06-27 was 4.5 for the game");
		}
	}
}
