using BeatTheStreak.Helpers;
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
		public bool Hitters { get; set; }

		private void DateLine()
		{
			Console.WriteLine($"Week starting {WeekStarts:yyyy-MM-dd}");
		}

		public PlayerGameLogViewModel DumpWeek(int playerNo)
		{
			SetOutput();
			DateLine();
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
				PlayerDump(totalLog, p);
			}
			DisplayTotals(totalLog);
			CloseOutput();
			return totalLog;
		}

		public void DumpCsv(
			string baseFolder,
			bool startNew)
		{
			var csv = new CsvFile(baseFolder)
			{
				FilePath = "GameLog",
				StartNew = startNew
			};
			if (csv.StartNew || csv.Exists().IsFailure)
				csv.Create();

			var roster = GetRoster(
				_rosterMaster,
				FantasyTeam,
				WeekStarts);
			foreach (var p in roster)
			{
				PlayerDumpToCsv(p,csv);
			}
		}

		private void PlayerDumpToCsv(string p, CsvFile csv)
		{
			_weekReport.WeekStarts = WeekStarts;
			_weekReport.DumpWeekToCsv(p, csv);
		}

		private void PlayerDump(
			PlayerGameLogViewModel totalLog, 
			string p)
		{
			_weekReport.Player = p;
			_weekReport.PlayerSlug = string.Empty;
			_weekReport.WeekStarts = WeekStarts;
			_weekReport.Hitters = Hitters;
			_weekReport.JerseyNumber = _rosterMaster.JerseyNumber(
				FantasyTeam,
				p,
				Hitters);
			totalLog.Add(_weekReport.DumpWeek(0));
		}
	}
}
