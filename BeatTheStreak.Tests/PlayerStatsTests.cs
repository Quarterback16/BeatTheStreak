using Application.StattlleShipApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeatTheStreak.Tests
{
    [TestClass]
    public class PlayerStatsTests
    {
        [TestMethod]
        public void PlayerStatsRequest_Ok()
        {
            var sut = new PlayerStatsRequest();
            var result = sut.Submit(
                queryDate: new DateTime(2018, 5, 4),
                playerSlug: "mlb-josh-bell");
            result.Dump();
            //Assert.IsTrue(result.Lineup.Count > 0, 
            //        "Lineup request should return batters");
        }
    }
}
