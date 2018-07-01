using BeatTheStreak.Implementations;
using BeatTheStreak.Repositories;
using BeatTheStreak.Tests.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class CalculateOpponentObaTests
	{
		private CalculateOpponentOba _sut;

		[TestInitialize]
		public void Setup()
		{
			var gameLogRepository = new GameLogRepository();
			_sut = new CalculateOpponentOba( 
				logger: new FakeLogger(),
				gameLogRepository: gameLogRepository);
		}

		[TestMethod]
		public void Calculate_ForIvanNovaIn3games_Returns300()
		{
			var result = _sut.CalculateOba(
				"mlb-ivan-nova",
				new DateTime( 2018, 6, 29),
				3);
			Assert.AreEqual(0.217M, result);
		}

		[TestMethod]
		public void Calculate_ForIvanNovaIn8games_Returns300()
		{
			var result = _sut.CalculateOba(
				playerSlug: "mlb-ivan-nova",
				gameDate: new DateTime(2018, 6, 29),
				daysBack: 8);
			Assert.AreEqual(0.160M, result);
		}
	}
}
