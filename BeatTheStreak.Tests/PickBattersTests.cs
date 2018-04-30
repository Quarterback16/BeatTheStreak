using Application;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeatTheStreak.Tests
{
    [TestClass]
    public class PickBattersTests
    {
        [TestMethod]
        public void PickBatters_ReturnsTwoBatters()
        {
            var sut = new PickBatters();
            var result = sut.Choose(DateTime.Now, 2);
            result.Dump();
            Assert.IsTrue(
                result.Selections.Count == 2,
                "There should be 2 batters returned");
        }
    }
}
