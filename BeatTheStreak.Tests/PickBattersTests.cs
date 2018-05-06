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
        public void PickBatters_ReturnsBestBatters()
        {
            const int numberDesired = 2;
            var pitcherRepo = new PitcherRepository();
            var lineupRepo = new LineupRepository();
            var picker = new RegularPicker(lineupRepo);
            var sut = new PickBatters(picker, lineupRepo, pitcherRepo);
            var result = sut.Choose(
                gameDate: DateTime.Now.AddDays(0),  // US Date
                numberRequired: numberDesired);
            result.Dump();
            Assert.IsTrue(
                result.Selections.Count == numberDesired,
                $"There should be {numberDesired} batters returned");
            foreach (var selection in result.Selections)
            {
                Assert.IsTrue(
                    Int32.Parse(selection.Batter.BattingOrder) > 0,
                    $"selection {selection.Batter.Name} is not a batter");
            }
        }
    }
}
