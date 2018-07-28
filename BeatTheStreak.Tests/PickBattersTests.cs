using Application;
using BeatTheStreak.Implementations;
using BeatTheStreak.Repositories;
using BeatTheStreak.Helpers;
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
		private DefaultPicker _sut;

		private ResultChecker _resultChecker;

		[TestInitialize]
		public void Setup()
		{
			var cache = new RedisCacheRepository(
				connectionString: "localhost,abortConnect=false",
				environment: "local",
				functionalArea: "bts",
				serializer: new XmlSerializer(),
				logger: new FakeCacheLogger(),
				expire: false);
			var pitcherRepo = new CachedPitcherRepository(
				new PitcherRepository(),
				cache);
			//var lineupRepo = new CachedLineupRepository(
			//	new LineupRepository(),
			//	cache);
			var lineupRepo = new LineupRepository();
			var statsRepo = new CachedPlayerStatsRepository(
				new PlayerStatsRepository(),
				cache);
			var teamStatsRepo = new CachedTeamStatsRepository(
				new TeamStatsRepository(),
				cache);
			var opposingPitcher = new OpposingPitcher(
				pitcherRepo);
			var lineupProjector = new LineupProjector(
				lineupRepo,
				opposingPitcher,
				new FakeLogger(),
				daysToGoBack: 10);
			var gameLogRepository = new CachedGameLogRepository(
				new GameLogRepository(),
				cache);
			var obaCalculator = new CalculateOpponentOba(
				new FakeLogger(),
				gameLogRepository);
			_resultChecker = new ResultChecker(statsRepo);
			var options = new Dictionary<string, string>
			{
				{ Constants.Options.HomePitchersOnly, "on" },
				{ Constants.Options.LineupPositions, "4" },
				{ Constants.Options.NoDaysOff, "off" },
				{ Constants.Options.DaysOffDaysBack, "3" },
				{ Constants.Options.HotBatters, "off" },
				{ Constants.Options.HotBattersDaysBack, "30" },
				{ Constants.Options.HotBattersMendozaLine, ".289" },
				{ Constants.Options.PitchersMendozaLine, ".259" },
				{ Constants.Options.PitcherDaysBack, "30" },
				{ Constants.Options.BattersToPick, "2" },
				{ Constants.Options.TeamClip, "off" },
				{ Constants.Options.PitchersTeamMendozaLine, ".555" },
				{ Constants.Options.BattersTeamMendozaLine, ".455" },
			};
			var pickerOptions = new PickerOptions(options);
			_sut = new DefaultPicker(
				pickerOptions,
				lineupRepo,
				pitcherRepo,
				statsRepo,
				teamStatsRepo,
				lineupProjector,
				obaCalculator,
				new FakeLogger());
		}

        [TestMethod]
        public void DefaultPicker_ReturnsBestBatters()
        {
			Console.WriteLine();
			Console.WriteLine("options:-"); ;
			foreach (var setting in _sut.PickerOptions.OptionStrings())
			{
				Console.WriteLine($"    {setting}");
			}
			Console.WriteLine();

			var gameDate = DateTime.Now.AddDays(-1);  // US Date
			var result = _sut.Choose(
                gameDate: gameDate,
                numberRequired: 2);
			if (Utility.GamePlayed(gameDate))
			{
				foreach (var selection in result.Selections)
				{
					selection.Result = _resultChecker.Result(
						selection.Batter,
						gameDate);
				}
			}
            result.Dump();
            Assert.IsTrue(
                result.Selections.Count == 2,
                "There should be 2 batters returned");
        }
	}
}
