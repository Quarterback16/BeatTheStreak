using System.Collections.Generic;
using Newtonsoft.Json;

namespace BeatTheStreak.Repositories.StattlleShipApi.Model
{
	public class RosteredPlayersDto
	{
		[JsonProperty("players")]
		public List<PlayerDto> Players { get; set; }

	}
}
