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
		[TestMethod]
		public void LineupProjector_ForGiants_Works()
		{
			var cache = new RedisCacheRepository(
				connectionString: "localhost,abortConnect=false",
				environment: "local",
				functionalArea: "bts",
				serializer: new XmlSerializer(),
				logger: new FakeLogger(),
				expire: false);
			var lineupRepo = new CachedLineupRepository(
				new LineupRepository(),
				cache);
			var testPitcher = new Pitcher
			{
				OpponentSlug = "mlb-sf",
				Throws = "R"
			};
			var sut = new LineupProjector(lineupRepo);
			var lineup = sut.ProjectLineup(
				testPitcher,
				lineupQueryDate: new System.DateTime(2018, 6, 13));
			Assert.IsNotNull(lineup);
			Assert.IsTrue(lineup.BattingAt("1").PlayerSlug.Equals("mlb-joe-panik"));
			Assert.IsTrue(lineup.BattingAt("2").PlayerSlug.Equals("mlb-buster-posey"));
		}
	}
}
