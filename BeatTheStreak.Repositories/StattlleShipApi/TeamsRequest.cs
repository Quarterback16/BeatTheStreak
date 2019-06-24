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
			Teams = new List<TeamDto>();
			for (int page = 1; page < 3; page++)
			{ 
				var httpWebRequest = CreateRequest(
					sport: "baseball",
					league: "mlb",
					apiRequest: "team_season_stats",
					queryParms: $"season_id={seasonSlug}&page={page}");

				var httpResponse = (HttpWebResponse)httpWebRequest
					.GetResponse();

				using (var streamReader = new StreamReader(
					httpResponse.GetResponseStream()))
				{
					var json = streamReader.ReadToEnd();
					var dto = JsonConvert.DeserializeObject<TeamStatsDto>(
						json);

					MergeTeams(Teams,dto.Teams);
				}
			}
			return Teams;
		}

		private void MergeTeams(
			List<TeamDto> teams1, 
			List<TeamDto> teams2)
		{
			foreach (var team in teams2)
			{
				if (!TeamListHas(teams1,team))
				{
					teams1.Add(team);
				}
			}
		}

		private bool TeamListHas(
			List<TeamDto> teams1, 
			TeamDto team)
		{
			var hasIt = false;
			foreach (var t in teams1)
			{
				if (t.Slug.Equals(team.Slug))
				{
					hasIt = true;
					break;
				}
			}
			return hasIt;
		}
	}
}
