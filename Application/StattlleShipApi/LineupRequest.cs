using Newtonsoft.Json;
using System.Collections.Generic;
using Domain;
using System.Net;
using System.IO;
using System.Diagnostics;
using System;
using Application.StattlleShipApi.Model;
using System.Linq;

namespace Application.StattlleShipApi
{
    public class LineupRequest : BaseApiRequest
    {
        public List<Batter> Batters { get; set; }

        public List<Batter> Submit(DateTime queryDate, string teamId)
        {
            var strDate = UniversalDate(queryDate);
            var result = new List<Batter>();

            var httpWebRequest = CreateRequest(
                sport: "baseball",
                league: "mlb",
                apiRequest: "lineups",
                queryParms: $"season_id=mlb-2018&on={strDate}&team_id={teamId}");

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var dto = JsonConvert.DeserializeObject<LineupsDto>(
                    streamReader.ReadToEnd());

                Players = dto.Players;
                Teams = dto.Teams;
                Games = dto.Games;

                foreach (var item in dto.Lineups)
                {
                    result.Add(MapDtoToBatter(item));
                };
            }
            Batters = result.OrderBy(o => o.BattingOrder).ToList();
            return Batters;
        }

        private Batter MapDtoToBatter(LineupDto dto)
        {
            var batter = new Batter
            {
                Name = GetName(dto.PlayerId, Players),
                TeamId = TeamFor(dto.TeamId, Teams),
                BattingOrder = dto.BattingOrder,
                LineupPosition = dto.LineupPosition,
                PositionAbbreviation = dto.PositionAbbreviation,
                Sequence = dto.Sequence,
                TeamSlug = TeamSlugFor(dto.TeamId, Teams),
                TeamName = TeamNameFor(dto.TeamId, Teams)
            };
            return batter;
        }

        public void Dump()
        {
            var pad = string.Empty;
            var lastPos = "X";
            foreach (var batter in Batters)
            {
                if (Int32.Parse(batter.BattingOrder) > 0)
                {
                    if (lastPos == batter.BattingOrder)
                        pad = "   ";
                    Console.WriteLine($"{pad} {batter}");
                    lastPos = batter.BattingOrder;
                    pad = string.Empty;
                }
            }
        }
    }
}
