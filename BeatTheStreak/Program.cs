using Application;
using BeatTheStreak.Implementations;
using BeatTheStreak.Repositories;
using BeatTheStreak.Helpers;
using Cache;
using System;
using System.Collections.Generic;
using System.IO;

namespace BeatTheStreak
{
    class Program
    {
        static void Main(string[] args)
		{
			CalculateVersionInfo(
				out Version versionInfo, 
				out DateTime computedDate);

			var logger = new Implementations.NLogAdaptor();
			logger.Info("-------------------------------------------------------------------------------------");
			logger.Info($@"Beat The Streak  ver:{
				versionInfo
				} built {
				computedDate.ToLongDateString()
				} Working Directory:{
				Directory.GetCurrentDirectory()
				}");

			var cacheLogger = new Cache.NLogAdaptor();
			var cache = new RedisCacheRepository(
				connectionString: "localhost,abortConnect=false",
				environment: "local",
				functionalArea: "bts",
				serializer: new XmlSerializer(),
				logger: cacheLogger,
				expire: false);
			var pitcherRepo = new CachedPitcherRepository(
				new PitcherRepository(),
				cache);
			var lineupRepo = new CachedLineupRepository(
				new LineupRepository(),
				cache);
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
				logger,
				daysToGoBack: 10);
			var resultChecker = new ResultChecker(statsRepo);
			var configReader = new ConfigReader();
			var mm = new MailMan2(configReader,logger);
			var mailer = new MailBatterReport(mailMan:mm,logger:logger);
			var gameLogRepository = new CachedGameLogRepository(
				new GameLogRepository(),
				cache);
			var obaCalculator = new CalculateOpponentOba(
				logger: logger,
				gameLogRepository: gameLogRepository);
			var options = new Dictionary<string, string>
			{
				{ Constants.Options.HomePitchersOnly, "on" },
				{ Constants.Options.LineupPositions, "3" },
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
			var sut = new DefaultPicker(
				pickerOptions,
				lineupRepo,
				pitcherRepo,
				statsRepo,
				teamStatsRepo,
				lineupProjector,
				obaCalculator,
				logger);
			var gameDate = DateTime.Now.AddDays(0);  // US Date

			try
			{
				var result = sut.Choose(
					gameDate: gameDate,
					numberRequired: 2);
				if (Utility.GamePlayed(gameDate))
				{
					foreach (var selection in result.Selections)
					{
						selection.Result = resultChecker.Result(
							selection.Batter,
							gameDate);
					}
				}
				result.Dump();
				mailer.MailReport(result);
				logger.Info("-------------------------------------------------------------------------------------");

			}
			catch (Exception ex)
			{
				logger.Error(ex.Message);
				Environment.FailFast(message: null);
				throw;
			}
		}

		private static void CalculateVersionInfo(
			out Version versionInfo, 
			out DateTime computedDate)
		{
			versionInfo = System.Reflection.Assembly.GetExecutingAssembly()
				.GetName().Version;
			var startDate = new DateTime(2018, 6, 23);
			var diffDays = versionInfo.Build;
			computedDate = startDate.AddDays(diffDays);
		}
	}
}
