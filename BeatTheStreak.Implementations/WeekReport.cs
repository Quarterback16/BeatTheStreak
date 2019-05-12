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
			var totalLog = new PlayerGameLogViewModel
			{
				HasGame = true,
			    IsBatter = Hitters,
			    IsPitcher = !Hitters
			};
			var playerSlug = Utility.PlayerSlug(Player);
			FantasyTeam = _rosterMaster.GetOwnerOf(Player);
			if (playerNo > 0) JerseyNumber = playerNo;
			for (int d = 0; d < 7; d++)
			{
				var queryDate = WeekStarts.AddDays(d);
				if (queryDate.Equals(DateTime.Now.Date.AddDays(0)))
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
					DisplayHeading(log);
				}
				Console.WriteLine(log.DateLine());
			}
			DisplayTotals(totalLog);
			return totalLog;
		}

	}
}
