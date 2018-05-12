using Application.Outputs;
using Application.StattlleShipApi.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Application.StattlleShipApi
{
    public class PlayerStatsRequest : BaseApiRequest
    {
        public List<PlayerSeasonStatsDto> PlayerStats { get; set; }

        public PlayerStatsViewModel Submit(DateTime queryDate, string playerSlug)
        {
            var result = new PlayerStatsViewModel
            {
                AsOf = queryDate
            };
            var strDate = UniversalDate(queryDate);

            var httpWebRequest = CreateRequest(
                sport: "baseball",
                league: "mlb",
                apiRequest: "player_season_stats",
                queryParms: $"season_id=mlb-2018&on={strDate}&player_id={playerSlug}");

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var dto = JsonConvert.DeserializeObject<PlayerStatsDto>(
                    streamReader.ReadToEnd());

                Teams = dto.Teams;
                PlayerStats = dto.PlayerStats;
            }
            result.Player = new Domain.Player
            {
                Name = playerSlug
            };
            if (PlayerStats.Count == 1)
            {
                SetBatterStats(result);
                SetPitcherStats(result);
            }
            else
            {
                //Console.WriteLine($"No Stats for {playerSlug} : {strDate}");
            }

            return result;
        }

        private void SetBatterStats(PlayerStatsViewModel result)
        {
            if (PlayerStats[0].AtBats != null)
                result.AtBats = Decimal.Parse(PlayerStats[0].AtBats);
            if (PlayerStats[0].Hits != null)
                result.Hits = Int32.Parse(PlayerStats[0].Hits);
            if (PlayerStats[0].BattingAverage != null)
                result.BattingAverage = Decimal.Parse(PlayerStats[0].BattingAverage);
        }

        private void SetPitcherStats(PlayerStatsViewModel result)
        {
            if (PlayerStats[0].Era != null)
                result.Era = Decimal.Parse(PlayerStats[0].Era);
            if (PlayerStats[0].Wins != null)
                result.Wins = Int32.Parse(PlayerStats[0].Wins);
            if (PlayerStats[0].InningsPitched != null)
                result.InningsPitched = Int32.Parse(PlayerStats[0].InningsPitched);
            if (PlayerStats[0].OpponentsBattingAverage != null)
                result.OpponentsBattingAverage = Decimal.Parse(PlayerStats[0].OpponentsBattingAverage);
            if (PlayerStats[0].GroundBallToFlyBallRatio != null)
                result.GroundBallTpFlyBallRatio = Decimal.Parse(PlayerStats[0].GroundBallToFlyBallRatio);
        }
    }
}
