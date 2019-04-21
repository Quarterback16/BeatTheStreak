using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using FbbEventStore;
using System;

namespace BeatTheStreak.Implementations
{
	public class TeamReport : PlayerReport, IWeekReport
	{
		private WeekReport _weekReport;
		private readonly IRosterMaster _rosterMaster;

		public TeamReport(
			WeekReport weekReport,
			IRosterMaster rosterMaster)
		{
			_weekReport = weekReport;
			_rosterMaster = rosterMaster;
		}

		public DateTime WeekStarts { get; set; }
		public string FantasyTeam { get; set; }

		public PlayerGameLogViewModel DumpWeek()
		{
			var totalLog = new PlayerGameLogViewModel
			{
				HasGame = true
			};
			var roster = GetRoster(
				_rosterMaster,
				FantasyTeam,
				WeekStarts);
			foreach (var p in roster)
			{
				_weekReport.Player = p;
				_weekReport.WeekStarts = WeekStarts;
				_weekReport.JerseyNumber = _rosterMaster.BatterNumber(FantasyTeam, p);
				totalLog.Add(_weekReport.DumpWeek());
			}
			DisplayTotals(totalLog);
			return totalLog;
		}

	}
}
