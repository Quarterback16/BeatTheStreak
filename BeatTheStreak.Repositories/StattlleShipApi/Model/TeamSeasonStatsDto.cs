using Newtonsoft.Json;

namespace BeatTheStreak.Repositories
{
	public class TeamSeasonStatsDto
	{
		[JsonProperty("id")]
		public string TeamId { get; set; }

		[JsonProperty("created_at")]
		public string CreatedAt { get; set; }

		[JsonProperty("updated_at")]
		public string UpdatedAt { get; set; }

		[JsonProperty("hits")]
		public string Hits { get; set; }

		[JsonProperty("at_bats")]
		public string AtBats { get; set; }

		[JsonProperty("wins")]
		public string Wins { get; set; }

		[JsonProperty("losses")]
		public string Losses { get; set; }

	}
}
