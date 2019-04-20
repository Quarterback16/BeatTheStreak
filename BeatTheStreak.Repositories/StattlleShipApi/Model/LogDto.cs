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

		[JsonProperty("home_runs")]
		public string HomeRuns { get; set; }

		[JsonProperty("runs")]
		public string Runs { get; set; }

		[JsonProperty("runs_batted_in")]
		public string RunsBattedIn { get; set; }

		[JsonProperty("total_bases")]
		public string TotalBases { get; set; }

		[JsonProperty("walks")]
		public string Walks { get; set; }

		[JsonProperty("strikeouts")]
		public string StrikeOuts { get; set; }

		[JsonProperty("stolen_bases")]
		public string StolenBases { get; set; }

		[JsonProperty("caught_stealing")]
		public string CaughtStealing { get; set; }

		[JsonProperty("player_id")]
        public string PlayerId { get; set; }

		[JsonProperty("earned_run_average")]
		public string ERA { get; set; }

		[JsonProperty("pitcher_earned_runs")]
		public string EarnedRuns { get; set; }

		[JsonProperty("pitcher_walks")]
		public string WalksAllowed { get; set; }

		[JsonProperty("wins")]
		public string Wins { get; set; }

		[JsonProperty("losses")]
		public string Losses { get; set; }

		[JsonProperty("saves")]
		public string Saves { get; set; }

		[JsonProperty("quality_starts")]
		public string QualityStarts { get; set; }

		[JsonProperty("pitcher_hits")]
		public string HitsAllowed { get; set; }

		[JsonProperty("pitcher_strikeouts")]
		public string BattersStruckOut { get; set; }

		[JsonProperty("innings_pitched")]
		public string InningsPitched { get; set; }

		[JsonProperty("whip")]
		public string Whip { get; set; }


		[JsonProperty("outs_pitched")]
		public string Outs { get; set; }

		public bool IsBatter()
		{
			return !IsPitcher();
		}

		public bool IsPitcher()
		{
			return (AtBats == null);
		}
	}
}
