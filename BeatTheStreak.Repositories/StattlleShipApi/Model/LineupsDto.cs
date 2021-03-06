﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace BeatTheStreak.Repositories
{
    public class LineupsDto
    {
        [JsonProperty("games")]
        public List<GameDto> Games { get; set; }

        [JsonProperty("home_teams")]
        public List<TeamDto> HomeTeams { get; set; }

        [JsonProperty("leagues")]
        public List<LeagueDto> Leagues { get; set; }

        [JsonProperty("away_teams")]
        public List<TeamDto> AwayTeams { get; set; }

        [JsonProperty("winning_teams")]
        public List<TeamDto> WinningTeams { get; set; }

        [JsonProperty("seasons")]
        public List<SeasonDto> Seasons { get; set; }

        [JsonProperty("venues")]
        public List<VenueDto> Venues { get; set; }

        [JsonProperty("officials")]
        public List<OfficialDto> Officials { get; set; }

        [JsonProperty("players")]
        public List<PlayerDto> Players { get; set; }

        [JsonProperty("teams")]
        public List<TeamDto> Teams { get; set; }

        [JsonProperty("lineups")]
        public List<LineupDto> Lineups { get; set; }
    }
}
