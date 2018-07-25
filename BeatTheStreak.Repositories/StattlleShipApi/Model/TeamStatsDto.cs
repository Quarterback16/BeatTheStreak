using Newtonsoft.Json;
using System.Collections.Generic;

namespace BeatTheStreak.Repositories
{
	public class TeamStatsDto
	{
		[JsonProperty("teams")]
		public List<TeamDto> Teams { get; set; }

		[JsonProperty("team_season_stats")]
		public List<TeamSeasonStatsDto> TeamStats { get; set; }
	}
}
