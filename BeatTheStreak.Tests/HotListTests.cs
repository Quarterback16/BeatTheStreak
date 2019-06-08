using BeatTheStreak.Implementations;
using BeatTheStreak.Repositories;
using BeatTheStreak.Tests.Fakes;
using Cache;
using FbbEventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class HotListTests
	{
		private HotList _sut;
		private IRosterMaster _rosterMaster;

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
			var actualRoster = new ActualRoster(
				lineupRepo);
			_rosterMaster = new FbbRosters(
				new FbbEventStore.FbbEventStore());
			_sut = new HotList(
				actualRoster,
				_rosterMaster);
		}
	}
}
