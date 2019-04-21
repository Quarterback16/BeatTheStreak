using BeatTheStreak.Helpers;
using BeatTheStreak.Implementations;
using BeatTheStreak.Repositories;
using FbbEventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class FantasyTests
	{
		[TestMethod]
		public void BattersStatsForOneDay()
		{
			var sut = new GameLogRequest();
			var result = sut.Submit(
				queryDate: new DateTime(2019, 4, 17),
				playerSlug: "mlb-michael-conforto");

			Console.WriteLine(result.DateHeaderLine());
			Console.WriteLine(result.DateLine());
		}

		[TestMethod]
		public void BattersStatsForTheWeek()
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
		public void PlayersCutForTheWeek()
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
		public void FantasyTeamHitterStatsForTheWeek()
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
		public void PitchersStatsForOneDay()
		{
			var sut = new GameLogRequest();
			var result = sut.Submit(
				queryDate: new DateTime(2019, 4, 16),
				playerSlug: "mlb-robbie-ray");

			Console.WriteLine(result.DateHeaderLine());
			Console.WriteLine(result.DateLine());
		}

		[TestMethod]
		public void PitchersStatsForTheWeek()
		{
			var sut = new WeekReport(
				new GameLogRequest())
			{
				WeekStarts = Utility.WeekStart(3),
				Player = "Nathan Eovaldi"
			};
			sut.DumpWeek();
		}

		[TestMethod]
		public void FantasyTeamPitcherStatsForTheWeek()
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
			sut.DoPitchers = true;
			sut.DumpWeek();
		}

		[TestMethod]
		public void PitcherStatsForTheSeason()
		{
			var sut = new SeasonReport(
					new GameLogRequest())
			{
				SeasonStarts = Utility.WeekStart(1),
				Player = "Luke Weaver"
			};
			sut.DumpSeason();
		}

		[TestMethod]
		public void MultiplePitcherStatsForTheSeason()
		{
			var sut = new SeasonReport(
					new GameLogRequest())
			{
				SeasonStarts = Utility.WeekStart(1),
				PlayerList = new List<string>
				{
					"Jose Quintana",
					"Collin McHugh",
					"James Paxton",
					"Spencer Turnbull",
					"Luke Weaver"
				}
			};
			sut.DumpPlayers();
		}


		[TestMethod]
		public void FantasyTeamPitcherStatsForTheSeason()
		{
			var sut = new TeamSeasonReport(
				new SeasonReport(
					new GameLogRequest()),
				new FbbRosters(
					new FbbEventStore.FbbEventStore()))
			{
				SeasonStarts = Utility.WeekStart(1),
				FantasyTeam = "CA"
			};
			sut.DoPitchers = true;
			sut.DumpSeason();
		}
	}
}
