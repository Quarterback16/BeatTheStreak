using System;
using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using FbbEventStore;

namespace BeatTheStreak.Implementations
{
	public class WeekReport : PlayerReport, IWeekReport
	{
		private readonly IGameLogRepository _gameLogRepository;
		private readonly IRosterMaster _rosterMaster;

		public bool Hitters { get; set; }

		public bool IncludePriorWeek { get; set; }

		public WeekReport(
			IGameLogRepository gameLogRepository,
			IRosterMaster rosterMaster)
		{
			_gameLogRepository = gameLogRepository;
			_rosterMaster = rosterMaster;
		}

		public DateTime WeekStarts { get; set; }


		public PlayerGameLogViewModel DumpWeek(int playerNo)
		{
			SetOutput();
			if (IncludePriorWeek)
				WeekStarts = WeekStarts.AddDays(-7);

			var totalLog = new PlayerGameLogViewModel
			{
				HasGame = true,
			    IsBatter = Hitters,
			    IsPitcher = !Hitters
			};
			if (string.IsNullOrEmpty(PlayerSlug))
			{
				PlayerSlug = Utility.PlayerSlug(Player);
			}
			FantasyTeam = _rosterMaster.GetOwnerOf(Player);
			if (playerNo > 0) JerseyNumber = playerNo;
			var daysToReport = 7;
			if (IncludePriorWeek)
				daysToReport += 7;
			for (int d = 0; d < daysToReport; d++)
			{
				var queryDate = WeekStarts.AddDays(d);
				if (queryDate.Equals(DateTime.Now.Date.AddDays(-1))
					&& Utility.ItsBeforeFour())
					break;

				var result = _gameLogRepository.Submit(
					queryDate: queryDate,
					playerSlug: PlayerSlug);

				if (result.IsSuccess)
				{
					var log = result.Value;
					log.IsBatter = Hitters;
					log.IsPitcher = !Hitters;
					totalLog.Add(log);
					if (log.HasGame)
					{
						totalLog.IsPitcher = log.IsPitcher;
						totalLog.IsBatter = log.IsBatter;
					}
					if (d == 0)
					{
						DisplayHeading(log, _rosterMaster);
					}
					Console.WriteLine(log.DateLine());
				}
			}
			DisplayTotals(totalLog);
			CloseOutput();
			return totalLog;
		}

		internal void DumpWeekToCsv(
			string p, 
			CsvFile csv)
		{
			Player = p;
			PlayerSlug = Utility.PlayerSlug(Player);

			FantasyTeam = _rosterMaster.GetOwnerOf(Player);
			var daysToReport = 7;
			for (int d = 0; d < daysToReport; d++)
			{
				var queryDate = WeekStarts.AddDays(d);
				if (queryDate.Equals(DateTime.Now.Date.AddDays(-1))
					&& Utility.ItsBeforeFour())
					break;

				var result = _gameLogRepository.Submit(
					queryDate: queryDate,
					playerSlug: PlayerSlug);

				if (result.IsSuccess)
				{
					var log = result.Value;
					log.IsBatter = Hitters;
					log.IsPitcher = !Hitters;
					Console.WriteLine(log.DateLine());
					WriteToCsv(csv, result);
				}
				else
					break;
			}
		}

		private void WriteToCsv(
			CsvFile csv, 
			Result<PlayerGameLogViewModel> result)
		{
			var gamelog = result.Value;
			var metrics = new string[11];
			metrics[0] = $"{Utility.UniversalDate(gamelog.AsOf)}";
			metrics[1] = PlayerSlug;
			metrics[2] = gamelog.Hits.ToString();
			metrics[3] = gamelog.AtBats.ToString();
			metrics[4] = gamelog.Runs.ToString();
			metrics[5] = gamelog.HomeRuns.ToString();
			metrics[6] = gamelog.TotalBases.ToString();
			metrics[7] = gamelog.RunsBattedIn.ToString();
			metrics[8] = gamelog.Walks.ToString();
			metrics[9] = gamelog.StrikeOuts.ToString();
			metrics[10] = gamelog.NetSteals().ToString();
			csv.AppendLine(metrics);
		}
	}
}
