using BeatTheStreak.Implementations;
using BeatTheStreak.Repositories;
using BeatTheStreak.Repositories.StattlleShipApi;
using BeatTheStreak.Tests.Fakes;
using Cache;
using FbbEventStore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class StartingPitcherTests
	{
		private StartingPitchers _sut;

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
			var es = new FbbEventStore.FbbEventStore();
			var rm = new FbbRosters(es);
			var startingPitcherRepository = new StartingPitchersRepository(
				rm);
			var gameLogRepository = new CachedGameLogRepository(
				new GameLogRepository(),
				cache);
			//var gameLogRepository = new GameLogRepository();

			_sut = new StartingPitchers(
				startingPitcherRepository,
				gameLogRepository,
				new FakeLogger());
		}

		//  Single pitcher query not required yet
		//[TestMethod]
		//public void StarterWasMaxScherzer_April25()
		//{
		//	var result = _sut.Starter(
		//		teamSlug: "mlb-was",
		//		gameDate: new DateTime(2019, 04, 25));
		//	Assert.AreEqual("Maxwell Scherzer", result.Name);
		//}

		[TestMethod]
		public void StartersReport_ReturnsAllThePitchersForTheDay()
		{
			var result = _sut.PitcherReport(
				new DateTime(2019, 5, 10));

			result.Dump();
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void HottiesReport_ReturnsAllTheHotFreeAgentPitchersForTheWeek()
		{
			var result = _sut.HotList(
				weekNo: 6);

			result.DumpHotList();
			Assert.IsNotNull(result);
		}

		[TestMethod]
		public void StarterRequest_ReturnsAllThePitchersForTheDay()
		{
			var es = new FbbEventStore.FbbEventStore();
			var rm = new FbbRosters(es);
			var sut = new StarterRequest(rm);
			var result = sut.Submit(
				new DateTime(2019, 4, 25));

			result.Dump();
			Assert.IsNotNull(result);
		}
	}
}
