using Application.StattlleShipApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeatTheStreak.Tests
{
    [TestClass]
    public class LineupTests
    {
        [TestMethod]
        public void Lineups_ReturnsMultipleBatters()
        {
            var sut = new LineupRequest();
            var result = sut.Submit(
                queryDate: new DateTime(2018,5,4),
                teamSlug: "mlb-pit");
            result.DumpLineup();
            Assert.IsTrue(result.Lineup.Count > 0, 
                "Lineup request should return batters");
        }
    }
}
