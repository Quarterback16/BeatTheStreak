using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace BeatTheStreak.Repositories
{
    public class PlayerStatsRequest : BaseApiRequest, IPlayerStatsRequest
    {
        public List<PlayerSeasonStatsDto> PlayerStats { get; set; }

        public PlayerStatsViewModel Submit(DateTime queryDate, string playerSlug)
        {
            var result = new PlayerStatsViewModel
            {
                AsOf = queryDate
            };
            var strDate = Utility.UniversalDate(queryDate);
            var qp = new System.Text.StringBuilder();
            qp.Append("season_id=mlb-2018");
            //qp.Append("&interval_type=today");
            qp.Append($"&on={strDate}");
            qp.Append($"&player_id={playerSlug}");
            var httpWebRequest = CreateRequest(
                sport: "baseball",
                league: "mlb",
                apiRequest: "player_season_stats",
                queryParms: qp.ToString() );

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
				var json = streamReader.ReadToEnd();

				var dto = JsonConvert.DeserializeObject<PlayerStatsDto>(json);

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
			if (PlayerStats[0].HitsAllowed != null)
				result.HitsAllowed = Int32.Parse(
					PlayerStats[0].HitsAllowed);
			if (PlayerStats[0].InningsPitched != null)
                result.InningsPitched = Decimal.Parse(
					PlayerStats[0].InningsPitched);
            if (PlayerStats[0].OpponentsBattingAverage != null)
                result.OpponentsBattingAverage = Decimal.Parse(
					PlayerStats[0].OpponentsBattingAverage);
            if (PlayerStats[0].GroundBallToFlyBallRatio != null)
                result.GroundBallTpFlyBallRatio = Decimal.Parse(
					PlayerStats[0].GroundBallToFlyBallRatio);
        }
    }
}
