using Application;
using BeatTheStreak.Models;
using BeatTheStreak.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class ResultCheckerTests
	{
		[TestMethod]
		public void ResultChecker_Works()
		{
			var gameLogRequest = new GameLogRequest();
			var sut = new ResultChecker(gameLogRequest);
			var batterReport = new BatterReport (new System.DateTime(2018, 05, 30))
			{
				Selections = new List<Selection>
				{
					new Selection
					{
						Batter = new Domain.Batter
						{
							PlayerSlug = "mlb-josh-bell"
						}
					}
				}
			};
			sut.Check(batterReport);
		}
	}
}
