using BeatTheStreak.Implementations;
using BeatTheStreak.Repositories;
using BeatTheStreak.Tests.Fakes;
using Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class OpposingPitcherTests
	{
		private OpposingPitcher _sut;

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
			var playerStatsRepository = new CachedPitcherRepository(
				new PitcherRepository(),
				cache);
			_sut = new OpposingPitcher(playerStatsRepository);
		}

		[TestMethod]
		public void OpposingPitcher_OnJun11_PiratesFacedCorbin()
		{
			var result = _sut.PitcherFacing(
				"mlb-pit", 
				new DateTime(2018, 06, 11));
			Assert.AreEqual(result.Name, "Patrick Corbin");
		}
	}
}
