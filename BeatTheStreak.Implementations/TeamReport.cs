using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using FbbEventStore;
using System;
using System.IO;

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
		public bool Hitters { get; set; }

		public PlayerGameLogViewModel DumpWeek(int playerNo)
		{
			SetOutput();
			var totalLog = new PlayerGameLogViewModel
			{
				HasGame = true,
				IsPitcher = ! Hitters,
				IsBatter = Hitters
			};
			var roster = GetRoster(
				_rosterMaster,
				FantasyTeam,
				WeekStarts);
			foreach (var p in roster)
			{
				_weekReport.Player = p;
				_weekReport.WeekStarts = WeekStarts;
				_weekReport.Hitters = Hitters;
				_weekReport.JerseyNumber = _rosterMaster.JerseyNumber(
					FantasyTeam, 
					p,
					Hitters);
				totalLog.Add(_weekReport.DumpWeek(0));
			}
			DisplayTotals(totalLog);
			CloseOutput();
			return totalLog;
		}

	}
}
