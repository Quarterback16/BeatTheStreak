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
            var result = sut.Submit(new DateTime(2018,5,4));  // US
            result.Dump();
            Assert.IsTrue(
                result.ProbablePitchers.Count > 0, 
                "Should return some pitchers");
        }

    }
}
