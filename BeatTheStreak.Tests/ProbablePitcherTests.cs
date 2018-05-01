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
            var result = sut.Submit(DateTime.Now);
            var i = 0;
            foreach (var pitcher in result)
            {
                Console.WriteLine($"{++i} {pitcher}");
            }
            Assert.IsTrue(result.Count>0, "No pitchers returned");
        }

    }
}
