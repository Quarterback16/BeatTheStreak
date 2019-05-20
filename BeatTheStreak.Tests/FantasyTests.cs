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
		const int K_CurrentWeek = 7;
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
				_gameLogRepository,
				_rosterMaster)
			{
				WeekStarts = Utility.WeekStart(K_CurrentWeek),
				Player = "Josh Reddick",
				Hitters = true
			};
			sut.DumpWeek(1);
		}

		[TestMethod]
		public void StreamHitters_ForTheWeek()
		{
			string[] streamers = new string[]
			{
				"Chris Davis",
				"Pablo Sandoval",
				"Scooter Gennett",
				"Austin Riley",
				"Mitch Moreland",
				"Oscar Mercado",
				"Kyle Schwarber",
				"Amed Rosario",
				"Randal Grichuk"
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
					WeekStarts = Utility.WeekStart(K_CurrentWeek),
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
		public void ExPlayers_ForTheWeek()
		{
			string[] playersDropped = new string[]
			{
				"Omar Narvaez",
				"Brandon Lowe",
				"Dansby Swanson",
				"Ryan Braun",
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
					WeekStarts = Utility.WeekStart(K_CurrentWeek),
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
					_gameLogRepository,
					//_cachedGameLogRepository,
					_rosterMaster),
				_rosterMaster)
			{
				WeekStarts = Utility.WeekStart(K_CurrentWeek),
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
				queryDate: new DateTime(2019, 4, 20),
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
				Hitters = false
			};
			sut.DoPitchers = true;
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
				Player = "Anthony DeSclafani"
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
					"Jordan Lyles",
					"Tyler Mahle",
					"Bradley Peacock",
					"Charles Morton",
					"Mike Fiers",
					"Christopher Paddack",
					"Martin Perez",
					"Anthony DeSclafani"
				}
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
				    "Wade Miley",
					"Corbin Martin",
					"Shaun Anderson",
					"Spencer Turnbull",
					"Julio Teheran",
					"Daniel Norris"
				}
			};
			sut.DumpPlayers();
		}

		[TestMethod]
		public void HittingProspects_ForTheWeek()
		{
			string[] prospects = new string[]
			{
				"Asdrubal Cabrera",
				"Jonathan Schoop",
				"Jorge Polanco",
				"Anthony Kemp",
				"Josh Reddick",
				"Ronny Rodriguez",
				"Brendan Rodgers",
				"Eric Sogard",
				"Kolten Wong",
				"DJ LeMahieu",
				"Niko Goodrum",
				"Hunter Dozier",
				"Nick Markakis",
				"John Smith Jr",
				"Adam Frazier",
				"Gregory Polanco",
				"Odubel Herrera",
				"Marwin Gonzalez",
				"C J Cron"
			};
			var i = 0;
			foreach (var player in prospects)
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
				weekNo: K_CurrentWeek);

			result.DumpHotList();
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void TheClosers()
		{
			var plyrs = new List<Closer>
			{
				new Closer("Greg Holland",       "AD", "medium"),
				new Closer( "A.J. Minter",       "AB", "committee" ),
				new Closer( "Mychal Givens",     "BO", "medium" ),
				new Closer( "Ryan Brasier",      "BRS",  "weak" ),
				new Closer( "Steve Cishek",       "CHC", "committee" ),
				new Closer( "Raisel Iglesias",   "CR", "strong" ),
				new Closer( "Brad Hand",         "CI", "strong" ),
				new Closer( "Wade Davis",        "COL", "strong" ),
				new Closer( "Alex Colome",       "CWS", "strong" ),
				new Closer( "Shane Greene",       "DT", "strong" ),
				new Closer( "Roberto Osuna",     "HA", "strong" ),
				new Closer( "Ian Kennedy",       "KC", "committee" ),
				new Closer( "Hansel Robles",     "LAA", "medium" ),
				new Closer( "Kenley Jansen",     "LAD", "strong" ),
				new Closer( "Sergio Romo",       "MIA", "medium" ),
				new Closer( "Josh Hader",        "MB", "medium" ),
				new Closer( "Blake Parker",      "MT", "committee" ),
				new Closer( "Edwin Diaz",        "NYM", "strong" ),
				new Closer( "Aroldis Chapman",   "NYY", "strong" ),
				new Closer( "Blake Trienen",     "OA", "strong" ),
				new Closer( "Hector Neris",      "PHP", "weak" ),
				new Closer( "Felipe Vazquez",    "PIT", "strong" ),
				new Closer( "Kirby Yates",       "SD", "strong" ),
				new Closer( "Anthony Swarzak",   "SM", "committee" ),
				new Closer( "Will Smith",        "SF", "strong" ),
				new Closer( "Jordan Hicks",      "SLC", "medium" ),
				new Closer( "Jose Alvarado",     "Tam", "committee" ),
				new Closer( "Chris Martin",       "TR", "weak" ),
				new Closer( "Ken Giles",         "TB", "strong" ),
				new Closer( "Sean Doolittle",    "Wsh", "strong" )
			};

			foreach (var p in plyrs)
			{
				var owner = _rosterMaster.GetOwnerOf(p.Name);
				Assert.IsNotNull(owner);
				if (owner=="FA")
					System.Console.WriteLine(
						$"{owner} owns {p} {p.Hold.ToUpper()}");
			}
		}
	}
}
