using BeatTheStreak.Helpers;
using BeatTheStreak.Implementations;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Repositories;
using BeatTheStreak.Tests.Fakes;
using Cache;
using Cache.Interfaces;
using FbbEventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class FantasyTests
	{
		private ICacheRepository _cache;
		private IGameLogRepository _gameLogRepository;
		private IGameLogRepository _cachedGameLogRepository;
		private IRosterMaster _rosterMaster;

		[TestInitialize]
		public void Setup()
		{
			_cache = new RedisCacheRepository(
				connectionString: "localhost,abortConnect=false",
				environment: "local",
				functionalArea: "bts",
				serializer: new XmlSerializer(),
				logger: new FakeCacheLogger(),
				expire: false);
			_gameLogRepository = new GameLogRepository();
			_cachedGameLogRepository = new CachedGameLogRepository(
						_gameLogRepository,
						_cache);
			_rosterMaster = new FbbRosters(
				new FbbEventStore.FbbEventStore());
		}

		[TestMethod]
		public void BattersStatsForOneDay()
		{
			var sut = new GameLogRequest();
			var result = sut.Submit(
				queryDate: new DateTime(2019, 5, 1),
				playerSlug: "mlb-raul-mondesi");

			Console.WriteLine(result.DateHeaderLine());
			Console.WriteLine(result.DateLine());
		}

		[TestMethod]
		public void BattersStatsForTheWeek()
		{
			var sut = new WeekReport(
				_cachedGameLogRepository,
				_rosterMaster)
			{
				WeekStarts = Utility.WeekStart(5),
				Player = "Josh Bell",
				Hitters = true
			};
			sut.DumpWeek(1);
		}

		[TestMethod]
		public void StreamHitters_ForTheWeek()
		{
			string[] streamers = new string[]
			{
				"Jose Martinez",
				"Alex Verdugo",
				"Carter Kieboom",
				"Kolten Wong",
				"Yonder Alonso",
				"Jorge Alfaro",
				"Jose Peraza",
				"Ryon Healy"
			};
			var i = 0;
			foreach (var player in streamers)
			{
				var sut = new WeekReport(
					_cachedGameLogRepository,
					_rosterMaster)
				{
					WeekStarts = Utility.WeekStart(5),
					Player = player,
					Hitters = true
				};
				sut.DumpWeek(++i);
			}
		}

		[TestMethod]
		public void BullOutfielders_ForTheWeek()
		{
			string[] streamers = new string[]
			{
				"Victor Robles",
				"Franmil Reyes",
				"Brian Goodwin",
				"Eric Thames"
			};
			var i = 0;
			foreach (var player in streamers)
			{
				var sut = new WeekReport(
					_cachedGameLogRepository,
					_rosterMaster)
				{
					WeekStarts = Utility.WeekStart(5),
					Player = player,
					Hitters = true
				};
				sut.DumpWeek(++i);
			}
		}

		[TestMethod]
		public void Top40_Outfielders_ForTheWeek()
		{
			string[] streamers = new string[]
			{
				"Mike Trout",
				"Christian Yelich",
				"Cody Bellinger",
				"Mookie Betts",
				"J.D. Martinez",
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
			var i = 0;
			foreach (var player in streamers)
			{
				var sut = new WeekReport(
					_cachedGameLogRepository,
					_rosterMaster)
				{
					WeekStarts = Utility.WeekStart(6),
					Player = player,
					Hitters = true
				};
				sut.DumpWeek(++i);
			}
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
					WeekStarts = Utility.WeekStart(6),
					Player = player,
					Hitters = true
				};
				sut.DumpWeek(++i);
			}
		}

		[TestMethod]
		public void ExPlayers_ForTheWeek()
		{
			string[] playersDropped = new string[]
			{
				"Enrique Hernandez",
				"Nomar Mazara",
				"Michael Brantley",
				"Ryon Healy",
				"Jonathan Villar",
				"Ryan McMahon",
				"Eric Hosmer",
				"Niko Goodrum",
				"Francisco Cervelli",
				"Kolten Wong",
				"Maikel Franco",
				"Jonathan Davis"
			};
			var i = 0;
			foreach (var player in playersDropped)
			{
				var sut = new WeekReport(
					_cachedGameLogRepository,
					_rosterMaster)
				{
					WeekStarts = Utility.WeekStart(5),
					Player = player,
					Hitters = true
				};
				sut.DumpWeek(++i);
			}
		}

		[TestMethod]
		public void FantasyTeamHitterStatsForTheWeek()
		{
			var sut = new TeamReport(
				new WeekReport(
					_cachedGameLogRepository,
					_rosterMaster),
				_rosterMaster)
			{
				WeekStarts = Utility.WeekStart(6),
				FantasyTeam = "CA",
				Hitters = true
			};
			sut.DumpWeek(0);
		}

		[TestMethod]
		public void PitchersStatsForOneDay()
		{
			var sut = new GameLogRequest();
			var result = sut.Submit(
				queryDate: new DateTime(2019, 4, 26),
				playerSlug: "mlb-max-scherzer");

			Console.WriteLine(result.DateHeaderLine());
			Console.WriteLine(result.DateLine());
		}

		[TestMethod]
		public void PitchersStatsForTheWeek()
		{
			var sut = new WeekReport(
				_gameLogRepository,
				_rosterMaster)
			{
				WeekStarts = Utility.WeekStart(4),
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
				WeekStarts = Utility.WeekStart(6),
				FantasyTeam = "CA",
				Hitters = false
			};
			sut.DoPitchers = true;
			sut.DumpWeek(0);
		}

		[TestMethod]
		public void PitcherStatsForTheSeason()
		{
			var sut = new SeasonReport(
					_cachedGameLogRepository,
					_rosterMaster	)
			{
				SeasonStarts = Utility.WeekStart(1),
				Player = "Caleb Smith"
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
		public void PitchingProspects_StatsForTheSeason()
		{
			var sut = new SeasonReport(
					_cachedGameLogRepository,
					_rosterMaster)
			{
				SeasonStarts = Utility.WeekStart(1),
				PlayerList = new List<string>
				{
					"Bradley Peacock",
					"Mike Fiers",
					"Christopher Paddack",
					"Jose Berrios",
					"Martin Perez",
					"Mike Minor",
					"Anthony De Sclafani"
				}
			};
			sut.DumpPlayers();
		}

		[TestMethod]
		public void HittingProspects_ForTheWeek()
		{
			string[] prospects = new string[]
			{
				"Eric Sogard",
			};
			var i = 0;
			foreach (var player in prospects)
			{
				var sut = new WeekReport(
					_cachedGameLogRepository,
					_rosterMaster)
				{
					WeekStarts = Utility.WeekStart(6),
					Player = player,
					Hitters = true
				};
				sut.DumpWeek(++i);
			}
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
				FantasyTeam = "CA"
			};
			sut.DoPitchers = true;
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
				weekNo: 6);

			result.DumpHotList();
			Assert.IsNotNull(result);
		}
	}
}
