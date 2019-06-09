using BeatTheStreak.Implementations;
using BeatTheStreak.Repositories;
using BeatTheStreak.Tests.Fakes;
using Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

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

			Assert.IsTrue(
				result
					.Where(item => item.Name.Equals("Joshua Bell")).Any());
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
				result
					.Where(item => item.Name.Equals("Joshua Bell")).Any());
			Assert.IsFalse(
				result
					.Where(item => item.Name.Equals("Christopher Archer")).Any());
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
				result
					.Where(item => item.Name.Equals("Joshua Bell")).Any());
			Assert.IsTrue(
				result
					.Where(item => item.Name.Equals("Christian Yelich")).Any());
			Assert.IsFalse(
				result
					.Where(item => item.Name.Equals("Christopher Archer")).Any());
		}
	}
}
