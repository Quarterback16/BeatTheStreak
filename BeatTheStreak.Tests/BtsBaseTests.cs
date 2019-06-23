using BeatTheStreak.Interfaces;
using BeatTheStreak.Repositories;
using BeatTheStreak.Tests.Fakes;
using Cache;
using Cache.Interfaces;
using FbbEventStore;

namespace BeatTheStreak.Tests
{
	public class BtsBaseTests
	{
		protected const int K_CurrentWeek = 12;
		protected ICacheRepository _cache;
		protected IGameLogRepository _gameLogRepository;
		protected IGameLogRepository _cachedGameLogRepository;
		protected IRosterMaster _rosterMaster;

		public void Initialize()
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
	}
}