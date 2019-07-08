using BeatTheStreak.Helpers;
using BeatTheStreak.Implementations;
using BeatTheStreak.Repositories;
using BeatTheStreak.Tests.Fakes;
using FbbEventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class FantasyTests : BtsBaseTests
	{
		[TestInitialize]
		public void Setup()
		{
			Initialize();
		}

		[TestMethod]
		public void BattersStatsForOneDay()
		{
			var sut = new GameLogRequest();
			var result = sut.Submit(
				queryDate: new DateTime(2019, 6, 20),
				playerSlug: "mlb-christian-yelich");
			if (result.IsSuccess)
			{
				Console.WriteLine(result.Value.DateHeaderLine());
				Console.WriteLine(result.Value.DateLine());
			}
			else
				Console.WriteLine(result.Error);
		}

		[TestMethod]
		public void BattersStatsForTheWeek()
		{
			var player = "Scott Kingery";
			var sut = new WeekReport(
//				_cachedGameLogRepository,
				_gameLogRepository,
				_rosterMaster)
			{
				WeekStarts = Utility.WeekStart(K_CurrentWeek),
				Player = player,
				Hitters = true,
				IncludePriorWeek = false,
				OutputFile = TestHelper.FileName(
						"Hitters",
						$"Batter-{player}",
						K_CurrentWeek)
			};
			sut.DumpWeek(playerNo: 1);
		}

		[TestMethod]
		public void StreamHitters_LastWeek()
		{
			//  lets us see how good a predictor is "Streamers of the Week"
			string[] streamers = new string[]
			{
				"Gregory Polanco",
				"Brendan Rodgers",
				"Adam Frazier",
				"Alex Verdugo",
				"Christian Walker",
				"Mitch Moreland",
				"Jarrod Dyson"
			};
			var sut = new WeekReportMulti(
					_cachedGameLogRepository,
					_rosterMaster,
					streamers,
					K_CurrentWeek)
			{
				OutputFile = TestHelper.FileName(
					"Hitters",
					"Streamers-Actual",
					K_CurrentWeek),
				IncludePriorWeek = false
			};
			sut.Execute();
		}

		[TestMethod]
		public void StreamHitters_Projected()
		{
			string[] streamers = new string[]
			{
				"Gregory Polanco",
				"Brendan Rodgers",
				"Adam Frazier",
				"Alex Verdugo",
				"Christian Walker",
				"Mitch Moreland",
				"Jarrod Dyson"
			};
			var sut = new WeekReportMulti(
					_cachedGameLogRepository,
					_rosterMaster,
					streamers,
					K_CurrentWeek)
			{
				OutputFile = TestHelper.FileName(
					"Hitters",
					"Streamers",
					K_CurrentWeek),
				IncludePriorWeek = true
			};
			sut.Execute();
		}

		[TestMethod]
		public void BullOutfielders_ForTheWeek()
		{
			string[] streamers = new string[]
			{
				"Victor Robles",
				"Brian Goodwin",
				"Eric Thames"
			};
			var sut = new WeekReportMulti(
					_cachedGameLogRepository,
					_rosterMaster,
					streamers,
					K_CurrentWeek)
			{
				OutputFile = TestHelper.FileName(
					"Hitters",
					"Stream-Hitters",
					K_CurrentWeek),
				IncludePriorWeek = true
			};
			sut.Execute();
		}

		[TestMethod]
		public void TheOutfielders()
		{
			string[] outfielders = new string[]
			{
				"Mike Trout",
				"Christian Yelich",
				"Cody Bellinger",
				"Mookie Betts",
				"J D Martinez",
				"Ronald Acuna Jr.",
				"Bryce Harper",
				"Whit Merrifield",
				"Rhys Hoskins",
				"Giancarlo Stanton",
				"George Springer",
				"Marcell Ozuna",
				"Charlie Blackmon",
				"Andrew Benintendi",
				"Juan Soto",
				"Aaron Judge",
				"Kris Bryant",
				"Khris Davis",
				"Victor Robles",
				"Eddie Rosario",
				"Starling Marte",
				"Joey Gallo",
				"Thomas Pham",
				"Lorenzo Cain",
				"Mitch Haniger",
				"Austin Meadows",
				"Michael Brantley",
				"Wil Myers",
				"Michael Conforto",
				"Andrew McCutchen",
				"Nicholas Castellanos",
				"Yasiel Puig",
				"Franmil Reyes",
				"Dee Gordon",
				"Shin-Soo Choo",
				"Justin Upton",
				"David Peralta",
				"Domingo Santana",
				"David Dahl",
				"Eloy Jimenez"
			};
			var sut = new WeekReportMulti(
					_cachedGameLogRepository,
					_rosterMaster,
					outfielders,
					K_CurrentWeek)
			{
				OutputFile = TestHelper.FileName(
					"Hitters",
					"Outfielders",
					K_CurrentWeek),
				IncludePriorWeek = false
			};
			sut.Execute();
		}

		[TestMethod]
		public void TopTeam_ForTheWeek()
		{
			string[] streamers = new string[]
			{
				"Mitch Garver",
				"Josh Bell",
				"Ketel Marte",
				"Hunter Dozier",
				"Dansby Swanson",
				"Dwight Smith"
			};
			var i = 0;
			foreach (var player in streamers)
			{
				var sut = new WeekReport(
					_cachedGameLogRepository,
					_rosterMaster)
				{
					WeekStarts = Utility.WeekStart(K_CurrentWeek),
					Player = player,
					Hitters = true
				};
				sut.DumpWeek(++i);
			}
		}

		[TestMethod]
		public void ExHitters_ForTheWeek()
		{
			string[] playersDropped = new string[]
			{
				"Franmil Reyes",
				"Joc Pederson",
				"Adam Jones",
				"Gregory Polanco",
				"Robinson Chirinos",
				"Brandon Lowe",
				"Dansby Swanson",
				"Enrique Hernandez",
				"Ryon Healy",
				"Brandon Lowe",
				"Shin-Soo Choo",
				"Niko Goodrum",
				"Kolten Wong",
				"Maikel Franco",
				"Jonathan Davis",
				"Ryan McMahon",
				"Rafael Devers",
				"Coery Dickerson",
				"Michael Brantley",
				"Nomar Mazara",
				"Eric Hosmer",
				"Jonathan Villar",
				"Francisco Cervelli",
			};
			var sut = new WeekReportMulti(
					_cachedGameLogRepository,
					_rosterMaster,
					playersDropped,
					K_CurrentWeek)
			{
				OutputFile = TestHelper.FileName(
					"Hitters",
					"CA-EX-Hitters",
					K_CurrentWeek),
				IncludePriorWeek = true
			};
			sut.Execute();
		}

		[TestMethod]
		public void FantasyTeamHitterStatsForTheWeek()
		{
			var week = Utility.CurrentWeek();
			var sut = new TeamReport(
				new WeekReport(
					_gameLogRepository,
					//_cachedGameLogRepository,
					_rosterMaster),
				_rosterMaster)
			{
				WeekStarts = Utility.WeekStart(week),
				FantasyTeam = "CA",
				Hitters = true
			};
			sut.OutputFile = TestHelper.FileName(
					"Hitters",
					"CA-Hitters",
					week);
			sut.DumpWeek(0);
		}

		[TestMethod]
		public void PitchersStatsForOneDay()
		{
			var sut = new GameLogRequest();
			var result = sut.Submit(
				queryDate: new DateTime(2019, 4, 20),
				playerSlug: "mlb-max-scherzer");
			if (result.IsSuccess)
			{
				Console.WriteLine(result.Value.DateHeaderLine());
				Console.WriteLine(result.Value.DateLine());
			}
		}

		[TestMethod]
		public void PitchersStatsForTheWeek()
		{
			var sut = new WeekReport(
				_gameLogRepository,
				_rosterMaster)
			{
				WeekStarts = Utility.WeekStart(K_CurrentWeek),
				Player = "Maxwell Scherzer"
			};
			sut.DumpWeek(1);
		}

		[TestMethod]
		public void FantasyTeamPitcherStatsForTheWeek()
		{
			var sut = new TeamReport(
				new WeekReport(
					_cachedGameLogRepository,
				    _rosterMaster),
				_rosterMaster)
			{
				WeekStarts = Utility.WeekStart(K_CurrentWeek),
				//WeekStarts = Utility.WeekStart(7),
				FantasyTeam = "CA",
				Hitters = false,
				DoPitchers = true,
				OutputFile = TestHelper.FileName(
					"Pitchers",
					"CA-Pitchers",
					K_CurrentWeek)
			};
			sut.DumpWeek(0);
		}

		[TestMethod]
		public void PitcherStatsForTheSeason()
		{
			var sut = new SeasonReport(
					_gameLogRepository,
					_rosterMaster	)
			{
				SeasonStarts = Utility.WeekStart(1),
				Player = "Jake Odorizzi"
			};
			sut.DumpSeason();
		}

		[TestMethod]
		public void AnomalyPitchersDiscovered_StatsForTheSeason()
		{
			var sut = new SeasonReport(
					_cachedGameLogRepository,
					_rosterMaster)
			{
				SeasonStarts = Utility.WeekStart(1),
				PlayerList = new List<string>
				{
					"Spencer Turnbull",
					"Mike Fiers",
					"Luke Weaver",
					"Jerad Eickhoff",
					"Caleb Smith"
				}
			};
			sut.DumpPlayers();
		}

		[TestMethod]
		public void ExPitchers_StatsForTheSeason()
		{
			var sut = new SeasonReport(
					_cachedGameLogRepository,
					_rosterMaster)
			{
				SeasonStarts = Utility.WeekStart(1),
				PlayerList = new List<string>
				{
					"Luke Weaver",
					"Jordan Lyles",
					"Alex Colome",
					"Jerad Eickhoff",
					"Spencer Turnbull",
					"Mike Fiers",
					"Jameson Taillon",
					"Matt Shoemaker",
					"Tyler Skaggs",
					"Eduardo Rodriguez"
				},
				DoPitchers = true,
				OutputFile = TestHelper.FileName(
					"Pitchers",
					"Ex-Pitchers",
					K_CurrentWeek)
			};
			sut.DumpPlayers();
		}

		[TestMethod]
		public void Pick4_StatsForTheSeason()
		{
			var sut = new SeasonReport(
					_cachedGameLogRepository,
					_rosterMaster)
			{
				SeasonStarts = Utility.WeekStart(1),
				PlayerList = new List<string>
				{
					"Trevor Bauer",
					"Griffin Canning",
					"Devin Smeltzer",
					"Bradley Peacock",
					"Caleb Smith"
				},
				DoPitchers = true,
				OutputFile = TestHelper.FileName(
					"Pitchers",
					"Lineup-Pick4",
					K_CurrentWeek)
			};
			sut.DumpPlayers();
		}

		[TestMethod]
		public void PitchingProspects_StatsForTheSeason()
		{
			var sut = new SeasonReport(
					_cachedGameLogRepository,
					_rosterMaster)
			{
				SeasonStarts = Utility.WeekStart(1),
				PlayerList = new List<string>
				{
					"Nick Pavetta",
					"Griffin Canning",
					"Jeffrey Hoffman",
					"Tanner Roark",
					"Felix Pena",
					"Trevor Richards",
					"Devin Smeltzer",
					"Corbin Martin",
					"Tyler Mahle",
					"Bradley Peacock",
					"Mike Fiers",
					"Christopher Paddack",
					"Martin Perez",
					"Anthony DeSclafani"
				},
				DoPitchers = true,
				OutputFile = TestHelper.FileName(
					"Pitchers",
					"Prospects",
					K_CurrentWeek)
			};
			sut.DumpPlayers();
		}

		[TestMethod]
		public void PitchingStreamers_StatsForTheSeason()
		{
			var sut = new SeasonReport(
					_cachedGameLogRepository,
					_rosterMaster)
			{
				SeasonStarts = Utility.WeekStart(1),
				PlayerList = new List<string>
				{
					"Corbin Martin",
					"Devin Smeltzer",
					"Chase Anderson"
				},
				OutputFile = TestHelper.FileName(
					"Pitchers",
					"Streamers",
					K_CurrentWeek)
			};
			sut.DumpPlayers();
		}

		[TestMethod]
		public void HittingProspects()
		{
			string[] prospects = new string[]
			{
				"Marwin Gonzalez",
				"Robinson Chirinos",
				"Louis Voit",
				"Brandon Lowe",
				"Jay Bruce",
			};
			var sut = new WeekReportMulti(
					_cachedGameLogRepository,
					_rosterMaster,
					prospects,
					K_CurrentWeek)
			{
				OutputFile = TestHelper.FileName(
					"Hitters",
					"Prospects",
					K_CurrentWeek),
				IncludePriorWeek = true
			};
			sut.Execute();
		}

		[TestMethod]
		public void LineupHittersChoice()
		{
			string[] prospects = new string[]
			{
				"Nomar Mazara",
				"Michael Conforto",
				"Joc Pederson",

			};
			var sut = new WeekReportMulti(
					_cachedGameLogRepository,
					_rosterMaster,
					prospects,
					K_CurrentWeek)
			{
				OutputFile = TestHelper.FileName(
					"Hitters",
					"LineupChoice",
					K_CurrentWeek),
				IncludePriorWeek = true
			};
			sut.Execute();
		}

		[TestMethod]
		public void TheCatchers()
		{
			string[] prospects = new string[]
			{
				"Jonathon Lucroy",  //  LAA
				"Mike Zunino",      //  TB
				"Danny Jansen",     //  Tor
				"Josh Phegley",     //  Oak
				"Robinson Chirinos",//  Hou
				"Austin Hedges",    //  SD
				"Christian Vazquez",//  Bos
				"James McCann",     //  CwS
				"Willians Astudillo",// MT
				"Yan Gomes",         // Wsh
				"Wellington Castillo",// ChW
				"Jason Castro",       // MT
				"Brian McCann",       // Atl
				"Isiah Kiner-Falefa", // Tex
				"Kurt Suzuki",        // Wsh
				"Tyler Flowers",      // Atl
				"Tony Wolters",       //  Col
				"Matt Wieters",       //  StL
				"Carson Kelly",       //  Ari
				"Roberto Perez",      // Cle
				"Grayson Greiner",    //  Det
				"Jeff Mathis",        // Tex
			};
			var sut = new WeekReportMulti(
					_cachedGameLogRepository,
					_rosterMaster,
					prospects,
					K_CurrentWeek)
			{
				OutputFile = TestHelper.FileName(
					"Hitters",
					"Catchers",
					K_CurrentWeek),
				IncludePriorWeek = true
			};
			sut.Execute();
		}

		[TestMethod]
		public void FantasyTeamPitcherStatsForTheSeason()
		{
			var sut = new SeasonReport(
					_cachedGameLogRepository,
					new FbbRosters(
						new FbbEventStore.FbbEventStore()))
			{
				SeasonStarts = Utility.WeekStart(1),
				FantasyTeam = "CA",
				DoPitchers = true,
			    OutputFile = TestHelper.FileName(
					"Pitchers",
					"CA-Pitchers-2019-",
					K_CurrentWeek)
			};
			sut.DumpPlayers();
		}

		[TestMethod]
		public void HottiesReport_ReturnsAllTheHotFreeAgentPitchersForTheWeek()
		{
			var _sut = new StartingPitchers(
				new StartingPitchersRepository(_rosterMaster),
				_cachedGameLogRepository,
				new FakeLogger());

			var result = _sut.HotList(
				weekNo: K_CurrentWeek);

			result.DumpHotList();
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void TheClosers()
		{
			var plyrs = new List<Closer>
			{
				new Closer( "Josh Hader",        "MB", "medium" ),
				new Closer( "Kirby Yates",       "SD", "strong" ),
				new Closer( "Brad Hand",         "CI", "strong" ),
				new Closer( "Aroldis Chapman",   "NYY", "strong" ),
				new Closer( "Kenley Jansen",     "LAD", "strong" ),
				new Closer( "Felipe Vazquez",    "PIT", "strong" ),
				new Closer( "Roberto Osuna",     "HA", "strong" ),
				new Closer( "Craig Kimbrel",       "CHC", "committee" ),
				new Closer( "Will Smith",        "SF", "strong" ),
				new Closer( "Ken Giles",         "Tor", "weak" ),
				new Closer( "Sean Doolittle",    "Wsh", "strong" ),
				new Closer( "Hector Neris",      "PHP", "medium" ),
				new Closer( "Shane Greene",       "DT", "strong" ),
				new Closer( "Alex Colome",       "CWS", "strong" ),
				new Closer( "Edwin Diaz",        "NYM", "strong" ),
				new Closer( "Hansel Robles",     "LAA", "committee" ),
				new Closer( "Carlos Martinez",    "SLC", "weak" ),
				new Closer( "Greg Holland",       "AD", "strong"),
				new Closer( "Ian Kennedy",       "KC", "medium" ),
				new Closer( "Raisel Iglesias",   "CR", "strong" ),
				new Closer( "Shawn Kelley",       "TR", "weak" ),
				new Closer( "Luke Jackson",       "AB", "medium" ),
				new Closer( "Roenis Elias",   "SM", "committee" ),
				new Closer( "Liam Hendricks",     "OA", "strong" ),
				new Closer( "Ryan Brasier",      "BRS",  "committee" ),
				new Closer( "Blake Parker",      "MT", "committee" ),
				new Closer( "Jose Alvarado",     "Tam", "committee" ),
				new Closer( "Wade Davis",        "COL", "strong" ),
				new Closer( "Sergio Romo",       "MIA", "medium" ),
				new Closer( "Shawn Armstrong",     "BO", "committee" ),
			};

			var i = 0;
			foreach (var p in plyrs)
			{
				i++;
				var owner = _rosterMaster.GetOwnerOf(p.Name);
				Assert.IsNotNull(owner);
				if (owner=="FA")
					Console.WriteLine(
						$"{p} {p.Hold.ToUpper()} ({i}) is available");
			}
		}
	}
}
