using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheStreak.Repositories
{
    public class LogDto
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("at_bats")]
        public string AtBats { get; set; }

        [JsonProperty("batting_average")]
        public string BatttingAverage { get; set; }

        [JsonProperty("hits")]
        public string Hits { get; set; }

        [JsonProperty("player_id")]
        public string PlayerId { get; set; }

    }
}
