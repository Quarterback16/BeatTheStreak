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
            var sut = new PlayerStatsRequest();
            var result = sut.Submit(
                queryDate: new DateTime(2018, 5, 4),
                playerSlug: "mlb-jameson-taillon");
            result.DumpPitcher();
        }

        [TestMethod]
        public void PlayerStatsRequest_BatterOk()
        {
            var sut = new PlayerStatsRequest();
            var result = sut.Submit(
                queryDate: new DateTime(2018, 5, 4),
                playerSlug: "mlb-josh-bell");
            result.Dump();
            Assert.IsTrue(
                result.BattingAverage.Equals(0.244M),
                "Josh Bells Season Bavg on 2018-05-04 was .244");
        }

        [TestMethod]
        public void PlayerStatsRequestOneDay_BatterOk()
        {
            var sut = new GameLogRequest();
            var result = sut.Submit(
                queryDate: new DateTime(2018, 5, 10),
                playerSlug: "mlb-josh-bell");
            result.Dump();
            Assert.IsTrue(
                result.BattingAverage.Equals(0.500M),
                "Josh Bells Bavg on 2018-05-11 was .500");
        }
    }
}
