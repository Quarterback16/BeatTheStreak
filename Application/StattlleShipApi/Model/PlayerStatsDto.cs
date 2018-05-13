using Newtonsoft.Json;
using System.Collections.Generic;

namespace Application.StattlleShipApi.Model
{
    public class PlayerStatsDto
    {
        [JsonProperty("seasons")]
        public List<SeasonDto> Seasons { get; set; }

        [JsonProperty("leagues")]
        public List<LeagueDto> Leagues { get; set; }

        [JsonProperty("teams")]
        public List<TeamDto> Teams { get; set; }

        [JsonProperty("divisions")]
        public List<DivisionDto> Divisions { get; set; }

        [JsonProperty("conferences")]
        public List<ConferenceDto> Conferences { get; set; }

        [JsonProperty("player_season_stats")]
        public List<PlayerSeasonStatsDto> PlayerStats { get; set; }
    }
}
