using Newtonsoft.Json;
using System.Collections.Generic;
using Domain;
using System.Net;
using System.IO;
using System.Diagnostics;
using System;
using Application.StattlleShipApi.Model;
using Application.StattlleShipApi;
using System.Linq;

namespace BeatTheStreak
{
    public class ProbablePitcherRequest : BaseApiRequest
    {
        public List<Pitcher> Submit(DateTime queryDate)
        {
            var strDate = UniversalDate(queryDate);
            //url: "https://api.stattleship.com/baseball/mlb/probable_pitchers?season_id=mlb-2018&on=2018-04-26
            var result = new List<Pitcher>();

            var httpWebRequest = CreateRequest(
                sport: "baseball",
                league: "mlb",
                apiRequest: "probable_pitchers",
                queryParms: $"season_id=mlb-2018&on={strDate}");

            var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var dto = JsonConvert.DeserializeObject<ProbablePitchersDto>(
                    streamReader.ReadToEnd());

                Players = dto.Players;
                Teams = dto.Teams;
                var i = 0;
                foreach (var item in dto.Teams)
                {
                    Console.WriteLine($"{++i} {item.Slug}");
                };
                Games = dto.Games;

                Console.WriteLine(dto);
                Debug.Write(dto);
                foreach (var item in dto.Pitchers)
                {
                    result.Add(MapDtoToPitcher(item));
                };
            }
            List<Pitcher> sortedByEra = result.OrderByDescending(o => o.Era).ToList();
            return sortedByEra;
        }

        private Pitcher MapDtoToPitcher(PitcherDto dto)
        {
            var pitcher = new Pitcher
            {
                Name = GetName(dto.PlayerId, Players),
                Wins = Int32.Parse(dto.Wins),
                Losses = Int32.Parse(dto.Losses),
                Era = Decimal.Parse(dto.Era),
                TeamId = TeamFor(dto.TeamId, Teams),
                NextOpponent = GameFor(dto.GameId, Games),
                OpponentId = OpponentTeamId(dto.GameId, dto.TeamId, Games)
            };
            return pitcher;
        }

        private string OpponentTeamId(string gameId, string teamId, List<GameDto> games)
        {
            string oppId = "???";
            foreach (var g in games)
            {
                if (g.Id == gameId)
                {
                    if (g.HomeTeamId == teamId)
                        oppId = g.xAwayTeamId;
                    else
                        oppId = g.HomeTeamId;
                    break;
                }
            }
            return oppId;
        }
    }
}
