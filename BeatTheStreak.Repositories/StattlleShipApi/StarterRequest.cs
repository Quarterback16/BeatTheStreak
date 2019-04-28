using BeatTheStreak.Helpers;
using BeatTheStreak.Models;
using Domain;
using FbbEventStore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace BeatTheStreak.Repositories.StattlleShipApi
{
	public class StarterRequest : BaseApiRequest
	{
		private readonly IRosterMaster _rosterMaster;

		public StarterRequest(
			IRosterMaster rosterMaster)
		{
			_rosterMaster = rosterMaster;
		}

		public StartersViewModel Submit(
			DateTime queryDate)
		{
			var result = new StartersViewModel
			{
				GameDate = queryDate
			};
			var strDate = Utility.UniversalDate(queryDate);

			var httpWebRequest = CreateRequest(
				sport: "baseball",
				league: "mlb",
				apiRequest: "starting_pitchers",
				queryParms: $"season_id=mlb-{queryDate.Year}&on={strDate}");

			var httpResponse
				= (HttpWebResponse) httpWebRequest.GetResponse();

			using (var streamReader = new StreamReader(
				httpResponse.GetResponseStream()))
			{
				var json = streamReader.ReadToEnd();
				var dto = JsonConvert.DeserializeObject<StarterDto>(
					json);

				Players = dto.Players;
				Teams = dto.Teams;
				Games = dto.Games;
				result = MapDtoToViewModel(
					dto.Pitchers,
					result);
			}
			return result;
		}

		private StartersViewModel MapDtoToViewModel(
			List<PitcherDto> dto,
			StartersViewModel result)
		{
	
			foreach (var p in dto)
			{
				var pitcher = new Pitcher
				{
					Name = GetName(p.PlayerId, Players),
					Slug = GetSlug(p.PlayerId, Players),
					Throws = GetHandedness(p.PlayerId, Players),
					Wins = int.Parse(p.Wins),
					Losses = int.Parse(p.Losses),
					Era = decimal.Parse(p.Era),
					TeamId = TeamSlugFor(p.TeamId, Teams),  // did not want to clear the cache
					TeamSlug = TeamSlugFor(p.TeamId, Teams),
					TeamName = TeamNameFor(p.TeamId, Teams),
					FantasyTeam = SetFantasyTeam(p),
				};
				result.Pitchers.Add(pitcher);
			}
			return result;
		}

		private string SetFantasyTeam(PitcherDto dto)
		{
			if (_rosterMaster == null)
				return " ";
			return _rosterMaster.GetOwnerOf(
				GetName(dto.PlayerId, Players));
		}
	}
}
