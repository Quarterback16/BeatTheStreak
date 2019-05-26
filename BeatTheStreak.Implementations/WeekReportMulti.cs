using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using FbbEventStore;
using System;
using System.IO;

namespace BeatTheStreak.Implementations
{
	public class WeekReportMulti : PlayerReport
	{
		private readonly IGameLogRepository _gameLogRepository;
		private readonly IRosterMaster _rosterMaster;
		private readonly string[] _players;
		private readonly int _week;

		public bool IncludePriorWeek { get; set; }

		public WeekReportMulti(
			IGameLogRepository gameLogRepository,
			IRosterMaster rosterMaster,
			string[] players,
			int week)
		{
			_gameLogRepository = gameLogRepository;
			_rosterMaster = rosterMaster;
			_players = players;
			_week = week;
		}

		public void Execute()
		{
			SetOutput();
			var i = 0;
			foreach (var player in _players)
			{
				var sut = new WeekReport(
					_gameLogRepository,
					_rosterMaster)
				{
					WeekStarts = Utility.WeekStart(_week),
					Player = player,
					Hitters = true,
					IncludePriorWeek = IncludePriorWeek
				};
				sut.DumpWeek(++i);
			}
			CloseOutput();
		}

	}
}
