﻿using Application;
using BeatTheStreak.Repositories;
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
            const int numberDesired = 2;
            var pitcherRepo = new PitcherRepository();
            var lineupRepo = new LineupRepository();
            var options = new Dictionary<string, string>
            {
                { Constants.Options.HomePitchersOnly, "on" },
                { Constants.Options.NoDaysOff, "off" },
                { Constants.Options.DaysOffDaysBack, "3" },
            };
            var sut = new DefaultPicker(options, lineupRepo, pitcherRepo);
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
