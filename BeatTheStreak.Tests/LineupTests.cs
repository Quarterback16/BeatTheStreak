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
                queryDate: DateTime.Now.AddDays(-1),
                teamSlug: "mlb-pit");
            sut.Dump();
            Assert.IsTrue(result.Count > 0, "No batters returned");
        }
    }
}
