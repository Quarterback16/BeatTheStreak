using Newtonsoft.Json;

namespace Application.StattlleShipApi.Model
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

        [JsonProperty("batting_average")]
        public string BattingAverage { get; set; }

        [JsonProperty("plate_appearence")]
        public string PlateAppearances { get; set; }

        [JsonProperty("era")]
        public string Era { get; set; }

        [JsonProperty("wins")]
        public string Wins { get; set; }

        [JsonProperty("innings_pitched")]
        public string InningsPitched { get; set; }

        [JsonProperty("ground_ball_to_fly_ball_ratio")]
        public string GroundBallToFlyBallRatio { get; set; }

        [JsonProperty("opponents_batting_average")]
        public string OpponentsBattingAverage { get; set; }

        [JsonProperty("whip")]
        public string Whip { get; set; }
    }
}
