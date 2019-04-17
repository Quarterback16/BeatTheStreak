using BeatTheStreak.Repositories;
using FbbEventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeatTheStreak.Tests
{
    [TestClass]
    public class ProbablePitcherTests
    {
        [TestInitialize]
        public void Setup()
        {
        }

        [TestMethod]
        public void ProbablePitchers_ReturnsMultiplePitchers()
        {
			var es = new FbbEventStore.FbbEventStore();
			var rm = new FbbRosters(es);
            var sut = new ProbablePitcherRequest(rm);
            var result = sut.Submit(DateTime.Now);
            result.Dump();
            Assert.IsTrue(
                result.ProbablePitchers.Count > 0, 
                "Should return some pitchers");
        }

        [TestMethod]
        public void ProbablePitchers_ReturnsMultipleHomePitchers()
        {
			var es = new FbbEventStore.FbbEventStore();
			var rm = new FbbRosters(es);
			var sut = new ProbablePitcherRequest(rm,homeOnly:true);
            var result = sut.Submit(new DateTime(2019, 4, 17));  // US
            result.Dump();
            Assert.IsTrue(
                result.ProbablePitchers.Count > 0,
                "Should return some pitchers");
        }

    }
}
