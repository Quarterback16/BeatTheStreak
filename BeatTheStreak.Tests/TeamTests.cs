using BeatTheStreak.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class TeamTests
	{
		[TestMethod]
		public void Teams_ReturnsTeamStats()
		{
			var sut = new TeamStatsRequest();
			var result = sut.Submit(
				queryDate: new DateTime(2018, 5, 4),
				teamSlug: "mlb-pit");
			//result.DumpTeam();
			Assert.IsNotNull(result);
			Assert.AreEqual(17, result.Wins);
		}
	}
}
