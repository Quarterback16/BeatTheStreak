using Newtonsoft.Json;
using System.Net;
using System.IO;
using System;
using BeatTheStreak.Helpers;
using System.Collections.Generic;
using BeatTheStreak.Models;

namespace BeatTheStreak.Repositories
{
	public class TeamStatsRequest : BaseApiRequest
	{
		public TeamStatsRequest()
		{
		}

		public TeamStatsViewModel Submit(DateTime queryDate, string teamSlug)
		{
			var result = new TeamStatsViewModel
			{
				AsOf = queryDate
			};

			var strDate = Utility.UniversalDate(queryDate);

			var httpWebRequest = CreateRequest(
				sport: "baseball",
				league: "mlb",
				apiRequest: "team_season_stats",
				queryParms: $"season_id=mlb-{queryDate.Year}&on={strDate}&team_id={teamSlug}");

			var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
			List<TeamSeasonStatsDto> teamSeasonStats;
			using (var streamReader = new StreamReader(
				httpResponse.GetResponseStream()))
			{
				var json = streamReader.ReadToEnd();
				var dto = JsonConvert.DeserializeObject<TeamStatsDto>(
					json);

				Teams = dto.Teams;
				teamSeasonStats = dto.TeamStats;
				if (teamSeasonStats.Count > 0)
				{
					if (teamSeasonStats[0].Wins != null)
						result.Wins = Int32.Parse(teamSeasonStats[0].Wins);
					if (teamSeasonStats[0].Losses != null)
						result.Losses = Int32.Parse(teamSeasonStats[0].Losses);
				}
			}
			result.TeamName = teamSlug;
			return result;
		}
	}
}
