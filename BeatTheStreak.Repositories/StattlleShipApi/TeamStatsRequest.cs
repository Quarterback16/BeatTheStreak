using Newtonsoft.Json;
using System.Net;
using System.IO;
using System;
using BeatTheStreak.Helpers;
using System.Collections.Generic;

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
				queryParms: $"season_id=mlb-2018&on={strDate}&team_id={teamSlug}");

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
				if (teamSeasonStats[0].Wins != null)
					result.Wins = Int32.Parse(teamSeasonStats[0].Wins);
			}
			result.TeamName = teamSlug;
			return result;
		}


	}

	public class TeamStatsViewModel
	{
		public DateTime AsOf { get; set; }
		public string TeamName { get; set; }
		public int Wins { get; set; }
	}
}
