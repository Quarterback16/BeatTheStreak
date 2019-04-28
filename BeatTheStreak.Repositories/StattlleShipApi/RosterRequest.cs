using BeatTheStreak.Repositories.StattlleShipApi.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BeatTheStreak.Repositories.StattlleShipApi
{
	public class RosterRequest : BaseApiRequest
	{
		public List<PlayerDto> LoadData(
			string seasonSlug,
			string teamSlug = "")
		{
			Players = new List<PlayerDto>();
			for (int p = 1; p < 4; p++)
			{
				var qp = new StringBuilder();
				qp.Append($"season_id={seasonSlug}");
				if (!string.IsNullOrEmpty(teamSlug))
					qp.Append($"&team_id={teamSlug}");
				qp.Append($"&page={p}");

				var httpWebRequest = CreateRequest(
					sport: "baseball",
					league: "mlb",
					apiRequest: "rosters",
					queryParms: qp.ToString());
				var httpResponse
					= (HttpWebResponse)httpWebRequest.GetResponse();

				using (var streamReader = new StreamReader(
					httpResponse.GetResponseStream()))
				{
					var json = streamReader.ReadToEnd();
					var dto = JsonConvert.DeserializeObject<RosteredPlayersDto>(
						json);

					if (dto.Players != null)
						Players.AddRange(dto.Players);
				}
			}

			return Players;
		}
	}
}
