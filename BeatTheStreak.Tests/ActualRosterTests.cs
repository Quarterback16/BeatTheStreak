using BeatTheStreak.Implementations;
using BeatTheStreak.Repositories;
using BeatTheStreak.Tests.Fakes;
using Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class ActualRosterTests
	{
		private ActualRoster _sut;

		[TestInitialize]
		public void Setup()
		{
			var cache = new RedisCacheRepository(
				connectionString: "localhost,abortConnect=false",
				environment: "local",
				functionalArea: "bts",
				serializer: new XmlSerializer(),
				logger: new FakeCacheLogger(),
				expire: false);
			var lineupRepo = new CachedLineupRepository(
				new LineupRepository(),
				cache);

			_sut = new ActualRoster(
				lineupRepo);
		}

		[TestMethod]
		public void ActualRoster_FindsCurrentPlayers()
		{
			var result = _sut.GetActualRoster(
				teamSlug: "mlb-pit",
				queryDate: new DateTime(2019,6,6),
				gamesBack: 1);
			Assert.IsTrue(result.Contains("Joshua Bell"));
		}

		[TestMethod]
		public void ActualRoster_FindsCurrentHitters()
		{
			var result = _sut.GetActualRoster(
				teamSlug: "mlb-pit",
				queryDate: new DateTime(2019, 6, 6),
				gamesBack: 1,
				battersOnly: true);
			Assert.IsTrue(
				result.Contains("Joshua Bell"));
			Assert.IsFalse(
				result.Contains("Christopher Archer"));
		}

		[TestMethod]
		public void ActualRoster_FindsCurrentHittersForManyTeams()
		{
			var result = _sut.GetActualRoster(
				teamSlugs: new List<string>
					{
						"mlb-pit",
						"mlb-mil"
					},
				queryDate: new DateTime(2019, 6, 6),
				gamesBack: 1,
				battersOnly: true);
			Assert.IsTrue(
				result.Contains("Joshua Bell"));
			Assert.IsTrue(
				result.Contains("Christian Yelich"));
			Assert.IsFalse(
				result.Contains("Christopher Archer"));
		}
	}
}
