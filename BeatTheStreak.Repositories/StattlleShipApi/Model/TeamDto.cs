using Newtonsoft.Json;
using System.Collections.Generic;

namespace BeatTheStreak.Repositories
{
    public class TeamDto
    {
        [JsonProperty("id")]
        public string TeamId { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("colors")]
        public List<string> Colors { get; set; }

        [JsonProperty("hashtag")]
        public string Hashtag { get; set; }

        [JsonProperty("hashtags")]
        public List<string> Hashtags { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("nickname")]
        public string NickName { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longtitude { get; set; }

        [JsonProperty("slug")]
        public string Slug { get; set; }

        [JsonProperty("division_id")]
        public string DivisionId { get; set; }

        [JsonProperty("league_id")]
        public string Leagueid { get; set; }

    }
}
