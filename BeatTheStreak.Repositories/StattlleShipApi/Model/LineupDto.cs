using Newtonsoft.Json;

namespace BeatTheStreak.Repositories
{
    public class LineupDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("inning")]
        public string Inning { get; set; }

        [JsonProperty("inning_half")]
        public string InningHalf { get; set; }

        [JsonProperty("batting_order")]
        public string BattingOrder { get; set; }

        [JsonProperty("lineup_position")]
        public string LineupPosition { get; set; }

        [JsonProperty("position_name")]
        public string PositionName { get; set; }

        [JsonProperty("position_abbreviation")]
        public string PositionAbbreviation { get; set; }

        [JsonProperty("sequence")]
        public string Sequence { get; set; }

        [JsonProperty("game_id")]
        public string GameId { get; set; }

        [JsonProperty("player_id")]
        public string PlayerId { get; set; }

        [JsonProperty("team_id")]
        public string TeamId { get; set; }
    }
}
