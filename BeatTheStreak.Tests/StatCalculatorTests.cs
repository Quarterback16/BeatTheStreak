using BeatTheStreak.Implementations;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Repositories;
using BeatTheStreak.Tests.Fakes;
using Cache;
using Cache.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class StatCalculatorTests
	{
		private StatCalculator _sut;
		private IGameLogRepository _gameLogRepository;
		private IGameLogRepository _cachedGameLogRepository;
		private ICacheRepository _cache;

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
			_gameLogRepository = new GameLogRepository();
			_cachedGameLogRepository = new CachedGameLogRepository(
						_gameLogRepository,
						_cache);

			_sut = new StatCalculator(
				//_gameLogRepository);
			    _cachedGameLogRepository);
		}

		[TestMethod]
		public void StatCalulator_WobaForYelich_Is345()
		{
			var result = _sut.Woba(
				"Christian Yelich",
				new DateTime(2019, 05, 27),
				new DateTime(2019, 06, 02));
			Assert.AreEqual(0.345M, result);
		}
	}
}
