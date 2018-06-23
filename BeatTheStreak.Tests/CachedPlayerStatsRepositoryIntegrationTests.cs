using BeatTheStreak.Interfaces;
using BeatTheStreak.Repositories;
using BeatTheStreak.Tests.Fakes;
using Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class CachedPlayerStatsRepositoryIntegrationTests
	{
		private IPlayerStatsRepository _sut;

		[TestInitialize]
		public void Setup()
		{
			var cache = new RedisCacheRepository(
				connectionString: "localhost,abortConnect=false",
				environment: "local",
				functionalArea: "bts",
				serializer: new JsonSerialiser(),
				logger: new FakeCacheLogger(),
				expire: false);
			var normalPlayerStatsRepository = new PlayerStatsRepository();
			_sut = new CachedPlayerStatsRepository(
				decoratedComponent: normalPlayerStatsRepository,
				cache: cache);
		}

		[TestMethod]
		public void CachedRepository_StoresResultsInCache()
		{
			var result = _sut.Submit(
				queryDate: new DateTime(2018, 6, 4),
				playerSlug: "mlb-francisco-cervelli");
			Assert.IsNotNull(result);
			Assert.AreEqual(40, result.Hits);
		}
	}
}
