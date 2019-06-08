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
			var playerSlug = Utility.PlayerSlug(Player);
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

				var log = _gameLogRepository.Submit(
					queryDate: queryDate,
					playerSlug: playerSlug);
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
					DisplayHeading(log,_rosterMaster);
				}
				Console.WriteLine(log.DateLine());
			}
			DisplayTotals(totalLog);
			CloseOutput();
			return totalLog;
		}


	}
}
