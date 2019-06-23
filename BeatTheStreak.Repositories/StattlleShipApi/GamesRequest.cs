using Newtonsoft.Json;
using System.Collections.Generic;
using Domain;
using System.Net;
using System.IO;
using System;
using System.Linq;
using BeatTheStreak.Models;
using BeatTheStreak.Helpers;

namespace BeatTheStreak.Repositories.StattlleShipApi
{
	public class GamesRequest : BaseApiRequest
	{
		public GamesViewModel Submit(
			string teamSlug,
			DateTime queryDate)
		{
			var result = new GamesViewModel();

			var strDate = Utility.UniversalDate(queryDate);

			var httpWebRequest = CreateRequest(
				sport: "baseball",
				league: "mlb",
				apiRequest: "games",
				queryParms: $"season_id=mlb-{queryDate.Year}&on={strDate}&team_id={teamSlug}&status=ended");
		//		queryParms: $"season_id=mlb-{queryDate.Year}&status=ended");

			var httpResponse 
				= (HttpWebResponse)httpWebRequest.GetResponse();

			using (var streamReader = new StreamReader(
				httpResponse.GetResponseStream()))
			{
				var json = streamReader.ReadToEnd();
				var dto = JsonConvert.DeserializeObject<GameDto>(
					json);

				//Players = dto.Players;
				//Teams = dto.Teams;
				//Games = dto.Games;
				result = MapDtoToViewModel(dto);
			}
			return result;
		}

		private GamesViewModel MapDtoToViewModel(GameDto dto)
		{
			var result = new GamesViewModel();
			result.ScoreLine = dto.ScoreLine;
			return result;
		}
	}
}
