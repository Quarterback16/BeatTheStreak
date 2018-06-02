using System;
using Newtonsoft.Json;

namespace BeatTheStreak.Repositories
{
    public class PitcherDto
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("wins")]
        public string Wins { get; set; }

        [JsonProperty("losses")]
        public string Losses { get; set; }

        [JsonProperty("era")]
        public string Era { get; set; }

        [JsonProperty("game_id")]
        public string GameId { get; set; }

        [JsonProperty("player_id")]
        public string PlayerId { get; set; }

        [JsonProperty("team_id")]
        public string TeamId { get; set; }
    }
}
