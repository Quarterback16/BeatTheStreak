using Application;
using BeatTheStreak.Models;
using BeatTheStreak.Repositories;
using BeatTheStreak.Tests.Fakes;
using Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class ResultCheckerTests
	{
		private ResultChecker _sut;

		[TestInitialize]
		public void Setup()
		{
			var cache = new RedisCacheRepository(
				connectionString: "localhost,abortConnect=false",
				environment: "local",
				functionalArea: "bts",
				serializer: new JsonSerialiser(),
				logger: new FakeLogger(),
				expire: false);
			var playerStatsRepository = new CachedPlayerStatsRepository(
				new PlayerStatsRepository(),
				cache );
			_sut = new ResultChecker(playerStatsRepository);
		}

		[TestMethod]
		public void ResultChecker_OnAdamJonesJun7_DidNotGetHit()
		{
			var batter = new Domain.Batter
			{
				PlayerSlug = "mlb-adam-jones"
			};
			var result = _sut.GotHit(batter, new System.DateTime(2018, 06, 07));
			Assert.IsFalse(result, "Adam Jones was 0 for 5 on Jun-7");
		}

		[TestMethod]
		public void ResultChecker_OnAdamJonesJun6_GotHit()
		{
			var batter = new Domain.Batter
			{
				PlayerSlug = "mlb-adam-jones"
			};
			var result = _sut.GotHit(batter, new System.DateTime(2018, 06, 06));
			Assert.IsTrue(result, "Adam Jones was 2 for 4 on Jun-6");
		}

		[TestMethod]
		public void ResultChecker_OnFrederickFreemanJun5_GotHit()
		{
			var batter = new Domain.Batter
			{
				PlayerSlug = "mlb-freddie-freeman"
			};
			var result = _sut.GotHit(batter, new System.DateTime(2018, 06, 05));
			Assert.IsTrue(result, "Freddie Freeman was 4 for 4 on Jun-5");
		}

		[TestMethod]
		public void ResultChecker_OnBrettGardnerJun3_HadNoAtBat()
		{
			var batter = new Domain.Batter
			{
				PlayerSlug = "mlb-brett-gardner"
			};
			var result = _sut.HadAtBat(batter, new System.DateTime(2018, 06, 03));
			Assert.IsFalse(result, "Brett Gardner had no official at bat on Jun-3");
		}

		[TestMethod]
		public void ResultChecker_OnBrettGardnerJun2_HadAtBat()
		{
			var batter = new Domain.Batter
			{
				PlayerSlug = "mlb-brett-gardner"
			};
			var result = _sut.HadAtBat(batter, new System.DateTime(2018, 06, 02));
			Assert.IsTrue(result, "Brett Gardner had an official at bat on Jun-2");
		}

	}
}
