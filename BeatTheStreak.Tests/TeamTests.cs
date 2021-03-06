﻿using BeatTheStreak.Repositories;
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
				queryDate: new DateTime(2019, 4, 19),
				teamSlug: "mlb-chc");
			result.DumpTeam();
			Assert.IsNotNull(result);
			Assert.AreEqual(10, result.Wins);
		}

		[TestMethod]
		public void Teams_ReturnsCorrectClip()
		{
			var sut = new TeamStatsRequest();
			var result = sut.Submit(
				queryDate: new DateTime(2018, 5, 4),
				teamSlug: "mlb-pit");
			result.DumpTeam();
			Assert.IsNotNull(result);
			Assert.AreEqual(.531M, result.Clip());
		}
	}
}
