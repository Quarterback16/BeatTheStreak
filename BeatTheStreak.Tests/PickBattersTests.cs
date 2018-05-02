using Application;
using Application.Pickers;
using Application.Repositories;
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
            var pitcherRepo = new PitcherRepository();
            var lineupRepo = new LineupRepository();
            var picker = new AlwaysLikePicker(lineupRepo);
            var sut = new PickBatters(picker,lineupRepo,pitcherRepo);
            var result = sut.Choose(DateTime.Now, 2);
            result.Dump();
            Assert.IsTrue(
                result.Selections.Count == 2,
                "There should be 2 batters returned");
            foreach (var selection in result.Selections)
            {
                Assert.IsTrue(
                    Int32.Parse(selection.Batter.BattingOrder) > 0,
                    $"selection {selection.Batter.Name} is not a batter");
            }
        }
    }
}
