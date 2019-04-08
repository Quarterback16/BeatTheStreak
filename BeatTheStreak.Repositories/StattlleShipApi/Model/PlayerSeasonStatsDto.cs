using Newtonsoft.Json;

namespace BeatTheStreak.Repositories
{
    public class PlayerSeasonStatsDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("statistics_on")]
        public string StatisticOn { get; set; }

        [JsonProperty("at_bats")]
        public string AtBats { get; set; }

        [JsonProperty("hits")]
        public string Hits { get; set; }

        [JsonProperty("doubles")]
        public string Doubles { get; set; }

		[JsonProperty("total_bases")]
		public string TotalBases { get; set; }

		[JsonProperty("batting_average")]
        public string BattingAverage { get; set; }

        [JsonProperty("plate_appearence")]
        public string PlateAppearances { get; set; }

        [JsonProperty("era")]
        public string Era { get; set; }

        [JsonProperty("wins")]
        public string Wins { get; set; }

		[JsonProperty("hits_allowed")]
		public string HitsAllowed { get; set; }

		[JsonProperty("innings_pitched")]
        public string InningsPitched { get; set; }

		[JsonProperty("innings_pitched_total_outs")]
		public string InningsPitchedTotalOuts { get; set; }

		[JsonProperty("ground_ball_to_fly_ball_ratio")]
        public string GroundBallToFlyBallRatio { get; set; }

        [JsonProperty("opponents_batting_average")]
        public string OpponentsBattingAverage { get; set; }

        [JsonProperty("whip")]
        public string Whip { get; set; }

		[JsonProperty("at_bats_per_home_run")]
		public string AbPerHr { get; set; }

		[JsonProperty("at_bats_per_strike_out")]
		public string AbPerSo { get; set; }

		[JsonProperty("walks_per_plate_experience")]
		public string WalkRatio { get; set; }

		[JsonProperty("pitchers_games_played")]
		public string PitcherGamesPlayed { get; set; }

		[JsonProperty("batters_faced")]
		public string BattersFaced { get; set; }

		[JsonProperty("complete_games")]
		public string CompleteGames { get; set; }

		[JsonProperty("quality_starts")]
		public string QualityStarts { get; set; }

		[JsonProperty("save_opportunities")]
		public string SaveOpportunities { get; set; }

		[JsonProperty("saves")]
		public string Saves { get; set; }

		[JsonProperty("blown_save")]
		public string BlownSaves { get; set; }
	}
}
