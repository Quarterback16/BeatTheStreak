using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeatTheStreak.Tests
{
    [TestClass]
    public class ProbablePitcherTests
    {

        [TestMethod]
        public void ProbablePitchers_ReturnsPitchers()
        {
            var sut = new ProbablePitcherRequest();
            var result = sut.Submit(new DateTime(2018, 4, 26));
            var i = 0;
            foreach (var pitcher in result)
            {
                Console.WriteLine($"{++i} {pitcher.Name}");
            }
            Assert.IsTrue(result.Count > 0, "No pitchers returned");
        }
    }
}
