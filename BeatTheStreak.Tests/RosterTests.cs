using BeatTheStreak.Repositories;
using BeatTheStreak.Repositories.StattlleShipApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class RosterTests
	{
		[TestMethod]
		public void TeamRoster_ForSeason2019_ReturnsDto()
		{
			var sut = new RosterRequest();
			var result = sut.LoadData(
				Constants.MlbSeasons.Season2019,
				"mlb-hou");
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Count > 0);
			DumpPlayerDto(result);
		}

		private static void DumpPlayerDto(
			System.Collections.Generic.List<PlayerDto> result)
		{
			var p = 0;
			foreach (var player in result)
			{
				//if (!player.Active)
				//	continue;
				p++;
				System.Console.WriteLine(
					$"{p:00} {player.RosterLine()}");
			}
		}
	}
}
