using Newtonsoft.Json;
using System.Collections.Generic;

namespace BeatTheStreak.Repositories.StattlleShipApi
{
	public class StarterDto
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

		[JsonProperty("starting_pitchers")]
		public List<PitcherDto> Pitchers { get; set; }

	}
}
