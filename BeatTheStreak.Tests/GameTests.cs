using BeatTheStreak.Repositories.StattlleShipApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;


namespace BeatTheStreak.Tests
{
	[TestClass]
	public class GameTests
	{
		[TestMethod]
		public void GamesRequest_ReturnsGameDto()
		{
			var sut = new GamesRequest();
			var result = sut.Submit(
				"mlb-was",
				new DateTime(2019, 4, 25));


			Assert.IsNotNull(result);
		}
	}
}
