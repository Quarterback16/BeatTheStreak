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
				playerSlug: "mlb-ivan-nova",
				gameDate: new DateTime( 2018, 6, 29),
				daysBack: 3);
			Assert.AreEqual(0.217M, result);
		}

		[TestMethod]
		public void Calculate_ForIvanNovaIn8games_Returns160()
		{
			var result = _sut.CalculateOba(
				playerSlug: "mlb-ivan-nova",
				gameDate: new DateTime(2018, 6, 29),
				daysBack: 8);
			Assert.AreEqual(0.160M, result);
		}

		[TestMethod]
		public void Calculate_ForJakeArriettaLast25days_Returns300()
		{
			var result = _sut.CalculateOba(
				playerSlug: "mlb-jake-arrieta",
				gameDate: new DateTime(2018, 7, 1),
				daysBack: 25);
			Assert.AreEqual(0.289M, result);
		}

		[TestMethod]
		public void Calculate_ForAntonioSenzatellaLast25days_ReturnsNewbie0()
		{
			var result = _sut.CalculateOba(
				playerSlug: "mlb-antonio-senzatela",
				gameDate: new DateTime(2018, 7, 3),
				daysBack: 25);
			Assert.AreEqual(0.0M, result);
		}
	}
}
