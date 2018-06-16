using Application;
using BeatTheStreak.Implementations;
using BeatTheStreak.Repositories;
using BeatTheStreak.Tests.Fakes;
using Cache;
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
			var cache = new RedisCacheRepository(
				connectionString: "localhost,abortConnect=false",
				environment: "local",
				functionalArea: "bts",
				serializer: new XmlSerializer(),
				logger: new FakeLogger(),
				expire: false);
            var pitcherRepo = new CachedPitcherRepository(
				new PitcherRepository(),
				cache);
            var lineupRepo = new CachedLineupRepository(
				new LineupRepository(),
				cache);
			var statsRepo = new CachedPlayerStatsRepository(
				new PlayerStatsRepository(),
				cache );
			var opposingPitcher = new OpposingPitcher(
				pitcherRepo	);
			var lineupProjector = new LineupProjector(
				lineupRepo,
				opposingPitcher,
				daysToGoBack: 10);
			var resultChecker = new ResultChecker(statsRepo);
			var options = new Dictionary<string, string>
            {
                { Constants.Options.HomePitchersOnly, "on" },
                { Constants.Options.NoDaysOff, "off" },
                { Constants.Options.DaysOffDaysBack, "3" },
				{ Constants.Options.HotBatters, "on" },
				{ Constants.Options.HotBattersDaysBack, "30" },
				{ Constants.Options.HotBattersMendozaLine, ".299" },
				{ Constants.Options.PitchersMendozaLine, ".221" },
			};
			var pickerOptions = new PickerOptions(options);
            var sut = new DefaultPicker(
				pickerOptions, 
				lineupRepo, 
				pitcherRepo,
				statsRepo,
				lineupProjector);
			var gameDate = DateTime.Now.AddDays(0);  // US Date
			var result = sut.Choose(
                gameDate: gameDate,
                numberRequired: 2);
			if (GamePlayed(gameDate))
			{
				foreach (var selection in result.Selections)
				{
					selection.Result = resultChecker.Result(
						selection.Batter,
						gameDate);
				}
			}
            result.Dump();
            Assert.IsTrue(
                result.Selections.Count == 2,
                "There should be 2 batters returned");
        }

		private bool GamePlayed(DateTime gameDate)
		{
			if ( DateTime.Now > gameDate.AddDays(1) )
			{
				return true;
			}
			return false;
		}
	}
}
