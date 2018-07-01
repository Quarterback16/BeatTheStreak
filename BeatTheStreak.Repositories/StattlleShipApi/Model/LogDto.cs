using Newtonsoft.Json;

namespace BeatTheStreak.Repositories
{
    public class LogDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

		[JsonProperty("game_started")]
		public string GameStarted { get; set; }

        [JsonProperty("at_bats")]
        public string AtBats { get; set; }

        [JsonProperty("batting_average")]
        public string BatttingAverage { get; set; }

        [JsonProperty("hits")]
        public string Hits { get; set; }

        [JsonProperty("player_id")]
        public string PlayerId { get; set; }

		[JsonProperty("earned_run_average")]
		public string ERA { get; set; }

		[JsonProperty("pitcher_earned_runs")]
		public string EarnedRuns { get; set; }

		[JsonProperty("pitcher_hits")]
		public string HitsAllowed { get; set; }

		[JsonProperty("outs_pitched")]
		public string Outs { get; set; }

	}
}
