using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheStreak.Repositories
{
    public class PlayerRankingDto
    {
        [JsonProperty("games")]
        public List<GameDto> Games { get; set; }

        [JsonProperty("teams")]
        public List<TeamDto> Teams { get; set; }

        [JsonProperty("home_teams")]
        public List<TeamDto> HomeTeams { get; set; }

        [JsonProperty("winning_teams")]
        public List<TeamDto> WinningTeams { get; set; }

        [JsonProperty("players")]
        public List<PlayerDto> Players { get; set; }
    }
}
