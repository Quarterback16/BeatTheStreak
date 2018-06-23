using BeatTheStreak.Implementations;
using BeatTheStreak.Repositories;
using BeatTheStreak.Tests.Fakes;
using Cache;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace BeatTheStreak.Tests
{
	[TestClass]
	public class LineupProjectorIntegrationTests
	{
		private LineupProjector _sut;

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
			var pitcherRepo = new CachedPitcherRepository(
				new PitcherRepository(),
				cache);
			var opposingPitcher = new OpposingPitcher(
				pitcherRepo);

			_sut = new LineupProjector(
				lineupRepo, 
				opposingPitcher,
				new FakeLogger(),
				daysToGoBack: 10);
		}

		[TestMethod]
		public void LineupProjector_ForGiants_Works()
		{
			var testPitcher = new Pitcher
			{
				OpponentSlug = "mlb-sf",
				Throws = "R"
			};
			var lineup = _sut.ProjectLineup(
				testPitcher,
				lineupQueryDate: new System.DateTime(2018, 6, 13));
			Assert.IsNotNull(lineup);
			Assert.IsTrue(lineup.BattingAt("1").PlayerSlug.Equals("mlb-joe-panik"));
			Assert.IsTrue(lineup.BattingAt("2").PlayerSlug.Equals("mlb-buster-posey"));
		}

		[TestMethod]
		public void LineupProjector_VersusLefty_Works()
		{
			var testPitcher = new Pitcher
			{
				OpponentSlug = "mlb-nym",
				Throws = "L"
			};
			var lineup = _sut.ProjectLineup(
				testPitcher,
				lineupQueryDate: new System.DateTime(2018, 6, 16));
			Assert.IsNotNull(lineup);
			lineup.DumpLineup();
			Assert.IsTrue(lineup.BattingAt("1").PlayerSlug.Equals("mlb-brandon-nimmo"));
			Assert.IsTrue(lineup.BattingAt("2").PlayerSlug.Equals("mlb-asdrubal-cabrera"));
			Assert.IsTrue(lineup.BattingAt("3").PlayerSlug.Equals("mlb-jose-bautista"));
		}

	}
}
