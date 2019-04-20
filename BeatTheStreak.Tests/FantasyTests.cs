using BeatTheStreak.Helpers;
using BeatTheStreak.Implementations;
using BeatTheStreak.Repositories;
using FbbEventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class FantasyTests
	{
		[TestMethod]
		public void CanDumpABattersStatsForOneDay()
		{
			var sut = new GameLogRequest();
			var result = sut.Submit(
				queryDate: new DateTime(2019, 4, 17),
				playerSlug: "mlb-michael-conforto");

			Console.WriteLine(result.DateHeaderLine());
			Console.WriteLine(result.DateLine());
		}

		[TestMethod]
		public void CanDumpABattersStatsForTheWeek()
		{
			var sut = new WeekReport(
				new GameLogRequest())
			{
				WeekStarts = new DateTime(2019, 4, 15),
				Player = "Michael Conforto"
			};
			sut.DumpWeek();
		}

		[TestMethod]
		public void CanDumpPlayersCutForTheWeek()
		{
			string[] playersDropped = new string[]
			{
				"Paul DeJong",
				"Nomar Mazara",
				"Michael Brantley",
				"Rafael Devers",
				"Ryan McMahon",
				"Eric Hosmer",
				"Kolten Wong"
			};
			foreach (var player in playersDropped)
			{
				var sut = new WeekReport(
					new GameLogRequest())
				{
					WeekStarts = Utility.WeekStart(3),
					Player = player
				};
				sut.DumpWeek();
			}
		}

		[TestMethod]
		public void CanDumpAFantasyTeamHitterStatsForTheWeek()
		{
			var sut = new TeamReport(
				new WeekReport(
					new GameLogRequest()),
				new FbbRosters(
					new FbbEventStore.FbbEventStore()))
			{
				WeekStarts = Utility.WeekStart(3),
				FantasyTeam = "CA"
			};
			sut.DumpWeek();
		}

		[TestMethod]
		public void CanDumpAPitchersStatsForOneDay()
		{
			var sut = new GameLogRequest();
			var result = sut.Submit(
				queryDate: new DateTime(2019, 4, 17),
				playerSlug: "mlb-nathan-eovaldi");

			Console.WriteLine(result.DateHeaderLine());
			Console.WriteLine(result.DateLine());
		}

		[TestMethod]
		public void CanDumpAPitchersStatsForTheWeek()
		{
			var sut = new WeekReport(
				new GameLogRequest())
			{
				WeekStarts = Utility.WeekStart(3),
				Player = "Nathan Eovaldi"
			};
			sut.DumpWeek();
		}
	}
}
