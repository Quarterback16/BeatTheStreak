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
		}

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
			var sut = new WeekReport(_cachedGameLogRepository)
			{
				WeekStarts = Utility.WeekStart(3),
				Player = "Freddie Freeman",
				Hitters = true
			};
			sut.DumpWeek();
		}

		[TestMethod]
		public void PlayersCutForTheWeek()
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
				"JD Davis"
			};
			foreach (var player in playersDropped)
			{
				var sut = new WeekReport(_cachedGameLogRepository)
				{
					WeekStarts = Utility.WeekStart(4),
					Player = player,
					Hitters = true
				};
				sut.DumpWeek();
			}
		}

		[TestMethod]
		public void FantasyTeamHitterStatsForTheWeek()
		{
			var sut = new TeamReport(
				new WeekReport(_cachedGameLogRepository),
				new FbbRosters(
					new FbbEventStore.FbbEventStore()))
			{
				WeekStarts = Utility.WeekStart(3),
				FantasyTeam = "CA",
				Hitters = true
			};
			sut.DumpWeek();
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
			var sut = new WeekReport(_gameLogRepository)
			{
				WeekStarts = Utility.WeekStart(4),
				Player = "Maxwell Scherzer"
			};
			sut.DumpWeek();
		}

		[TestMethod]
		public void FantasyTeamPitcherStatsForTheWeek()
		{
			var sut = new TeamReport(
				new WeekReport(_cachedGameLogRepository),
				new FbbRosters(
					new FbbEventStore.FbbEventStore()))
			{
				WeekStarts = Utility.WeekStart(4),
				FantasyTeam = "CA",
				Hitters = false
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
