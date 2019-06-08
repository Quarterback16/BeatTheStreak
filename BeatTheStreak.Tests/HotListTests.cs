using BeatTheStreak.Helpers;
using BeatTheStreak.Implementations;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Repositories;
using BeatTheStreak.Tests.Fakes;
using Cache;
using Cache.Interfaces;
using FbbEventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class HotListTests
	{
		private HotList _sut;
		private IRosterMaster _rosterMaster;
		private IGameLogRepository _gameLogRepository;
		private IGameLogRepository _cachedGameLogRepository;
		private ICacheRepository _cache;
		private IStatCalculator _statCalculator;

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
			var lineupRepo = new CachedLineupRepository(
				new LineupRepository(),
				_cache);
			var actualRoster = new ActualRoster(
				lineupRepo);
			_gameLogRepository = new GameLogRepository();
			_cachedGameLogRepository = new CachedGameLogRepository(
						_gameLogRepository,
						_cache);
			_statCalculator = new StatCalculator(
				_cachedGameLogRepository);
			_rosterMaster = new FbbRosters(
				new FbbEventStore.FbbEventStore());
			_sut = new HotList(
				actualRoster,
				_rosterMaster,
				_statCalculator);
		}

		[TestMethod]
		public void HotList_ReturnsHotPlayersFromOneTeam()
		{
			var result = _sut.GetHotList(
				teamSlugs: new List<string>
				{
					"mlb-pit"
				},
				queryDate: Utility.WeekStart(10).AddDays(-1),
				gamesBack: 3);
			Assert.IsTrue(result.Count > 1);
		}
	}
}
