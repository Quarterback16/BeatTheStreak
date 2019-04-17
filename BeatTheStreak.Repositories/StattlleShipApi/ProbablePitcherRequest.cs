using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System;
using System.Linq;
using Domain;
using BeatTheStreak.Models;
using BeatTheStreak.Helpers;
using FbbEventStore;

namespace BeatTheStreak.Repositories
{
    public class ProbablePitcherRequest : BaseApiRequest
    {
		private readonly IRosterMaster _rosterMaster;
        public Dictionary<string,Pitcher> TeamList { get; set; }

        public PlayerStatsRequest PlayerStatsRequest { get; set; }

        public DateTime GameDate { get; set; }

        public bool HomeOnly { get; set; }

        public ProbablePitcherRequest(
			IRosterMaster rosterMaster,
			bool homeOnly = false)
		{
            PlayerStatsRequest = new PlayerStatsRequest();
            HomeOnly = homeOnly;
			_rosterMaster = rosterMaster;
        }

        public ProbablePitcherViewModel Submit(DateTime queryDate)
        {
            var result = new ProbablePitcherViewModel
            {
                GameDate = queryDate,
                HomeOnly = HomeOnly
            };
            GameDate = queryDate;

            var strDate = Utility.UniversalDate(queryDate);
            //url: "https://api.stattleship.com/baseball/mlb/probable_pitchers?season_id=mlb-2018&on=2018-04-26
            var pitchers = new List<Pitcher>();
            TeamList = new Dictionary<string,Pitcher>();
            var httpWebRequest = CreateRequest(
                sport: "baseball",
                league: "mlb",
                apiRequest: "probable_pitchers",
                queryParms: $"season_id=mlb-{queryDate.Year}&on={strDate}");

            var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
				var json = streamReader.ReadToEnd();

				var dto = JsonConvert.DeserializeObject<ProbablePitchersDto>(
                    json);

                Players = dto.Players;
                Teams = dto.Teams;
                Games = dto.Games;

                foreach (var item in dto.Pitchers)
                {
                    // take the last pitcher
                    TryAddPitcher(item);
                };
            }
            foreach (KeyValuePair<string, Pitcher> pair in TeamList)
            {
                var p = pair.Value;
                if (HomeOnly && p.Away)
                   continue;
                
                pitchers.Add(p);
            }
            result.ProbablePitchers = pitchers.OrderByDescending(
                o => o.OpponentsBattingAverage).ToList();
            return result;
        }

        private Dictionary<string, Pitcher> InitaliseTeams(List<TeamDto> teams)
        {
            var d = new Dictionary<string, Pitcher>();
            foreach (TeamDto item in teams)
            {
                d.Add(item.TeamId, new Pitcher());
            }
            return d;
        }

        private void TryAddPitcher( 
            PitcherDto item)
        {
            var pitcher = MapDtoToPitcher(item);
            if (TeamList.ContainsKey(item.TeamId))
                TeamList[item.TeamId] = pitcher;
            else
                TeamList.Add(item.TeamId, pitcher);
        }

        private Pitcher MapDtoToPitcher(PitcherDto dto)
		{
			var oppId = OpponentTeamId(dto.GameId, dto.TeamId, Games);
			var pitcher = new Pitcher
			{
				Name = GetName(dto.PlayerId, Players),
				Slug = GetSlug(dto.PlayerId, Players),
				Throws = GetHandedness(dto.PlayerId, Players),
				Wins = int.Parse(dto.Wins),
				Losses = int.Parse(dto.Losses),
				Era = decimal.Parse(dto.Era),
				TeamId = TeamSlugFor(dto.TeamId, Teams),  // did not want to clear the cache
				TeamSlug = TeamSlugFor(dto.TeamId, Teams),
				TeamName = TeamNameFor(dto.TeamId, Teams),
				NextOpponent = GameFor(dto.GameId, Games),
				FantasyTeam = SetFantasyTeam(dto),
				OpponentId = oppId,
				OpponentSlug = TeamSlugFor(oppId, Teams),
				OpponentsBattingAverage = GetOpponentsBattingAverage(
					GetPlayerSlug(dto.PlayerId, Players)),
				Away = IsAway(dto.TeamId, dto.GameId, Games)
			};
			return pitcher;
		}

		private string SetFantasyTeam(PitcherDto dto)
		{
			if (_rosterMaster == null)
				return " ";
			return _rosterMaster.GetOwnerOf(GetName(dto.PlayerId, Players));
		}

		private bool IsAway(string teamId, string gameId, List<GameDto> games)
        {
            foreach (var g in games)
            {
                if (g.Id == gameId)
                {
                    if (g.AwayTeamId == teamId)
                        return true;
                }
            }
            return false;
        }

        private decimal GetOpponentsBattingAverage(string pitcherSlug)
        {
            var result = PlayerStatsRequest.Submit(
                queryDate: GameDate.AddDays(-1),
                playerSlug: pitcherSlug);
            return result.OpponentsBattingAverage;
        }

        private string OpponentTeamId(string gameId, string teamId, List<GameDto> games)
        {
            string oppId = "???";
            foreach (var g in games)
            {
                if (g.Id == gameId)
                {
                    if (g.HomeTeamId == teamId)
                        oppId = g.AwayTeamId;
                    else
                        oppId = g.HomeTeamId;
                    break;
                }
            }
            return oppId;
        }

    }
}
