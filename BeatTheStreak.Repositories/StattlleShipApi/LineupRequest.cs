using Newtonsoft.Json;
using System.Collections.Generic;
using Domain;
using System.Net;
using System.IO;
using System;
using System.Linq;
using BeatTheStreak.Models;
using BeatTheStreak.Helpers;

namespace BeatTheStreak.Repositories
{
    public class LineupRequest : BaseApiRequest
    {
        public List<Batter> Batters { get; set; }

        public PlayerStatsRequest PlayerStatsRequest { get; set; }

        public DateTime GameDate { get; set; }

        public LineupRequest()
        {
            PlayerStatsRequest = new PlayerStatsRequest();
        }

        public LineupViewModel Submit(DateTime queryDate, string teamSlug)
        {
            var result = new LineupViewModel
            {
                GameDate = queryDate
            };
            GameDate = queryDate;

            var strDate = Utility.UniversalDate(queryDate);
            var lineup = new List<Batter>();

            var httpWebRequest = CreateRequest(
                sport: "baseball",
                league: "mlb",
                apiRequest: "lineups",
                queryParms: $"season_id=mlb-2018&on={strDate}&team_id={teamSlug}");
		
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
                    if (Int32.Parse(item.BattingOrder) > 0 )
                        lineup.Add(MapDtoToBatter(item));
                };
            }
            Batters = lineup.OrderBy(o => o.BattingOrder).ToList();
            SetSubs();
            result.Lineup = Batters;
            result.TeamName = teamSlug;
            return result;
        }

        private void SetSubs()
        {
            var lastPos = "X";
            foreach (var batter in Batters)
            {
                if (Int32.Parse(batter.BattingOrder) > 0)
                {
                    if (lastPos == batter.BattingOrder)
                    {
                        batter.IsSub = true;
                    }
                    lastPos = batter.BattingOrder;
                }
            }
        }

        private Batter MapDtoToBatter(LineupDto dto)
        {
            var playerSlug = GetPlayerSlug(dto.PlayerId, Players);
            var batter = new Batter
            {
                PlayerSlug = playerSlug,
                Name = GetName(dto.PlayerId, Players),
                TeamId = TeamFor(dto.TeamId, Teams),
				Bats = GetBats(dto.PlayerId, Players ),
                BattingOrder = dto.BattingOrder,
                LineupPosition = dto.LineupPosition,
                PositionAbbreviation = dto.PositionAbbreviation,
                Sequence = dto.Sequence,
                TeamSlug = TeamSlugFor(dto.TeamId, Teams),
                TeamName = TeamNameFor(dto.TeamId, Teams),
                BattingAverage = GetBattingAverage(playerSlug)
            };
            return batter;
        }

        private decimal GetBattingAverage(string batterSlug)
        {
            var result = PlayerStatsRequest.Submit(
                queryDate: GameDate,
                playerSlug: batterSlug);
            return result.BattingAverage;
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
                    Console.WriteLine($"{pad} {batter} {batter.PlayerSlug}");
                    lastPos = batter.BattingOrder;
                    pad = string.Empty;
                }
            }
        }
    }
}
