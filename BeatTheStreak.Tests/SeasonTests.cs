using BeatTheStreak.Repositories.StattlleShipApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class SeasonTests
	{
		[TestMethod]
		public void Teams_ForSeason2019_ReturnsTeamsDto()
		{
			var sut = new TeamsRequest();
			var result = sut.LoadData(Constants.MlbSeasons.Season2019)
				.OrderBy(x=>x.Name);

			Assert.IsNotNull(result);
			foreach (var team in result)
				System.Console.WriteLine(team);
		}
	}
}
