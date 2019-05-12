using BeatTheStreak.Repositories;
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
                queryDate: new DateTime(2019,5,8),
                teamSlug: "mlb-min");
            result.DumpLineup();
            Assert.IsTrue(result.Lineup.Count > 0, 
                "Lineup request should return batters");
        }
    }
}
