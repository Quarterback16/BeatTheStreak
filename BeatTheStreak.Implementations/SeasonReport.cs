using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using FbbEventStore;
using System;
using System.Collections.Generic;

namespace BeatTheStreak.Implementations
{
	public class SeasonReport : PlayerReport, ISeasonReport
	{
		private readonly IGameLogRequest _gameLogRequestor;
		public DateTime SeasonStarts { get; set; }

		public List<string> PlayerList { get; set; }

		public SeasonReport(
			IGameLogRequest gameLogRequestor)
		{
			_gameLogRequestor = gameLogRequestor;
		}

		public void DumpPlayers()
		{
			foreach (var player in PlayerList)
			{
				Player = player;
				DumpSeason();
			}
		}

		public PlayerGameLogViewModel DumpSeason()
		{
			var totalLog = new PlayerGameLogViewModel
			{
				HasGame = true
			};
			var playerSlug = Utility.PlayerSlug(Player);
			for (int d = 0; d < (6*30); d++)
			{
				var queryDate = SeasonStarts.AddDays(d);
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
				if (log.HasGame)
				{
					Console.WriteLine(log.DateLine());
				}
			}
			DisplayTotals(totalLog);
			return totalLog;
		}
	}
}
