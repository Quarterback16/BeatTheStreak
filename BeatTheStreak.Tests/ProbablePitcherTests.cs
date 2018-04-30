using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeatTheStreak.Tests
{
    [TestClass]
    public class ProbablePitcherTests
    {

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

        [TestMethod]
        public void ProbablePitchers_ForApril27_Returns36Pitchers()
        {
            var sut = new ProbablePitcherRequest();
            var result = sut.Submit(new DateTime(2018, 4, 27));
            var i = 0;
            foreach (var pitcher in result)
            {
                Console.WriteLine($"{++i} {pitcher}");
            }
            Assert.IsTrue(result.Count == 36, "No pitchers returned");
        }
    }
}
