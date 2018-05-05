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

        [JsonProperty("plate_appearence")]
        public string PlateAppearances { get; set; }

        [JsonProperty("era")]
        public string Era { get; set; }

        [JsonProperty("wins")]
        public string Wins { get; set; }

        [JsonProperty("innings_pitched")]
        public string InningsPitched { get; set; }
    }
}
