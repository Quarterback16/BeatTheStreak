using BeatTheStreak.Repositories;
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
            var sut = new ProbablePitcherRequest();
            var result = sut.Submit(new DateTime(2018,5,6));  // US
            result.Dump();
            Assert.IsTrue(
                result.ProbablePitchers.Count > 0, 
                "Should return some pitchers");
        }

        [TestMethod]
        public void ProbablePitchers_ReturnsMultipleHomePitchers()
        {
            var sut = new ProbablePitcherRequest(homeOnly:true);
            var result = sut.Submit(new DateTime(2018, 5, 6));  // US
            result.Dump();
            Assert.IsTrue(
                result.ProbablePitchers.Count > 0,
                "Should return some pitchers");
        }

    }
}
