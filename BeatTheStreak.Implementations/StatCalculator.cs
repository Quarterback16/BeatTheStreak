using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Domain;
using System;


namespace BeatTheStreak.Implementations
{
	public class StatCalculator : IStatCalculator
	{
		private readonly IGameLogRepository _gameLogRepository;

		public StatCalculator(
			IGameLogRepository gameLogRepository)
		{
			_gameLogRepository = gameLogRepository;
		}

		public decimal Woba(
			string playerName, 
			DateTime startDate, 
			DateTime endDate)
		{
			var totalLog = new PlayerGameLogViewModel
			{
				HasGame = true,
				IsBatter = true,
				IsPitcher = false
			};
			var playerSlug = Utility.PlayerSlug(playerName);
			decimal stat = TallyLogs(
				startDate, 
				endDate, 
				totalLog, 
				playerSlug);
			return stat;
		}

		public decimal WobaBySlug(
			string playerSlug,
			DateTime startDate,
			DateTime endDate)
		{
			var totalLog = new PlayerGameLogViewModel
			{
				HasGame = true,
				IsBatter = true,
				IsPitcher = false
			};
			decimal stat = TallyLogs(
				startDate,
				endDate,
				totalLog,
				playerSlug);
			return stat;
		}

		public decimal AbBySlug(
			string playerSlug,
			DateTime startDate,
			DateTime endDate)
		{
			var totalLog = new PlayerGameLogViewModel
			{
				HasGame = true,
				IsBatter = true,
				IsPitcher = false
			};
			TallyLogs(
				startDate,
				endDate,
				totalLog,
				playerSlug);
			return totalLog.AtBats;
		}

		private decimal TallyLogs(
			DateTime startDate,
			DateTime endDate, 
			PlayerGameLogViewModel totalLog, 
			string playerSlug)
		{
			decimal stat;
			var daysToReport = DaysInRange(
				endDate,
				startDate);
			var queryDate = startDate;
			for (int d = 0; d < daysToReport; d++)
			{
				var log = _gameLogRepository.Submit(
					queryDate: queryDate,
					playerSlug: playerSlug);

				totalLog.Add(log);
				if (log.HasGame)
				{
					totalLog.IsPitcher = log.IsPitcher;
					totalLog.IsBatter = log.IsBatter;
				}
				queryDate = queryDate.AddDays(1);
				if (queryDate.Equals(DateTime.Now.Date.AddDays(-1))
					&& Utility.ItsBeforeFour())
					break;
			};
			totalLog.WOBA = totalLog.Woba();
			stat = totalLog.WOBA;
			return stat;
		}

		public decimal Woba(
			Player player, 
			DateTime startDate,
			DateTime endDate)
		{
			return Woba(
				player.Name,
				startDate,
				endDate);
		}

		private double DaysInRange(
			DateTime endDate, 
			DateTime startDate)
		{
			TimeSpan difference = endDate - startDate;
			return difference.TotalDays + 1;
		}
	}
}
