using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using FbbEventStore;
using System;

namespace BeatTheStreak.Implementations
{
	public class TeamSeasonReport : PlayerReport, ISeasonReport
	{
		private SeasonReport _seasonReport;
		private readonly IRosterMaster _rosterMaster;
		public DateTime SeasonStarts { get; set; }

		public TeamSeasonReport(
			SeasonReport seasonReport,
			IRosterMaster rosterMaster)
		{
			_seasonReport = seasonReport;
			_rosterMaster = rosterMaster;
		}

		public PlayerGameLogViewModel DumpSeason()
		{
			var totalLog = new PlayerGameLogViewModel
			{
				HasGame = true
			};
			System.Collections.Generic.List<string> roster = GetRoster(
				_rosterMaster,
				FantasyTeam,
				SeasonStarts);
			foreach (var p in roster)
			{
				_seasonReport.Player = p;
				_seasonReport.SeasonStarts = SeasonStarts;
//				_seasonReport.JerseyNumber = _rosterMaster.BatterNumber(FantasyTeam, p);
				totalLog.Add(_seasonReport.DumpSeason());
			}
			DisplayTotals(totalLog);
			return totalLog;
		}
	}
}
