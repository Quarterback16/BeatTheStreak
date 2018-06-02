using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatTheStreak.Repositories
{
    public class GameLogDto
    {
        [JsonProperty("games")]
        public List<GameDto> Games { get; set; }

        [JsonProperty("teams")]
        public List<TeamDto> Teams { get; set; }

        [JsonProperty("home_teams")]
        public List<TeamDto> HomeTeams { get; set; }

        [JsonProperty("leagues")]
        public List<LeagueDto> Leagues { get; set; }

        [JsonProperty("away_teams")]
        public List<TeamDto> AwayTeams { get; set; }

        [JsonProperty("winning_teams")]
        public List<TeamDto> WinningTeams { get; set; }

        [JsonProperty("players")]
        public List<PlayerDto> Players { get; set; }

        [JsonProperty("game_logs")]
        public List<LogDto> Logs { get; set; }



    }
}
