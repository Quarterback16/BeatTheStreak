using Application;
using Application.Pickers;
using Application.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BeatTheStreak.Tests
{
    [TestClass]
    public class PickBattersTests
    {
        [TestMethod]
        public void DefaultPicker_ReturnsBestBatters()
        {
            const int numberDesired = 4;
            var pitcherRepo = new PitcherRepository();
            var lineupRepo = new LineupRepository();
            var picker = new RegularPicker(lineupRepo);
            var options = new Dictionary<string, string>
            {
                { Constants.Options.HomePitchersOnly, "on" },
                { "dayOff", "Off" }
            };
            var sut = new DefaultPicker(options, picker, lineupRepo, pitcherRepo);
            var result = sut.Choose(
                gameDate: DateTime.Now.AddDays(0),  // US Date
                numberRequired: numberDesired);
            result.Dump();
            Assert.IsTrue(
                result.Selections.Count == numberDesired,
                $"There should be {numberDesired} batters returned");
            foreach (var selection in result.Selections)
            {
                Assert.IsTrue(selection.Batter.IsBatter(),
                    $"selection {selection.Batter.Name} is not a batter");
            }
        }
    }
}
