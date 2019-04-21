﻿using System;
using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;

namespace BeatTheStreak.Implementations
{
	public class WeekReport : PlayerReport, IWeekReport
	{
		private readonly IGameLogRequest _gameLogRequestor;

		public WeekReport(
			IGameLogRequest gameLogRequestor)
		{
			_gameLogRequestor = gameLogRequestor;
		}

		public DateTime WeekStarts { get; set; }


		public PlayerGameLogViewModel DumpWeek()
		{
			var totalLog = new PlayerGameLogViewModel
			{
				HasGame = true
			};
			var playerSlug = Utility.PlayerSlug(Player);
			for (int d = 0; d < 7; d++)
			{
				var queryDate = WeekStarts.AddDays(d);
				if (queryDate.Equals(DateTime.Now.Date.AddDays(-1)))
					break;

				var log = _gameLogRequestor.Submit(
					queryDate: queryDate,
					playerSlug: playerSlug);
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
