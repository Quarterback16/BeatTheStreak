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
                queryDate: new DateTime(2018, 4, 26),
                teamId: "mlb-tor");
            sut.Dump();
            Assert.IsTrue(result.Count > 0, "No batters returned");
        }
    }
}
