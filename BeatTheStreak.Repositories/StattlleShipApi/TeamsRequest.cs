using BeatTheStreak.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace BeatTheStreak.Repositories.StattlleShipApi
{
	public class TeamsRequest : BaseApiRequest
	{
		public List<TeamDto> LoadData(string seasonSlug)
		{
			var response = new TeamsViewModel();

			var httpWebRequest = CreateRequest(
				sport: "baseball",
				league: "mlb",
				apiRequest: "team_season_stats",
				queryParms: $"season_id={seasonSlug}");

			var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

			using (var streamReader = new StreamReader(
				httpResponse.GetResponseStream()))
			{
				var json = streamReader.ReadToEnd();
				var dto = JsonConvert.DeserializeObject<TeamStatsDto>(
					json);

				Teams = dto.Teams;
			}
			return Teams;
		}
	}
}
