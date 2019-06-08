using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
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
			decimal stat = 0.0M;
			var totalLog = new PlayerGameLogViewModel
			{
				HasGame = true,
				IsBatter = true,
				IsPitcher = false
			};
			var playerSlug = Utility.PlayerSlug(playerName);
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

		private double DaysInRange(
			DateTime endDate, 
			DateTime startDate)
		{
			TimeSpan difference = endDate - startDate;
			return difference.TotalDays + 1;
		}
	}
}
