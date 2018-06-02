using BeatTheStreak.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace BeatTheStreak.Repositories
{
    public class DailyRankingRequest : BaseApiRequest
    {
        public List<PlayerSeasonStatsDto> PlayerStats { get; set; }

        public PlayerStatsViewModel Submit(DateTime queryDate, string playerSlug)
        {
            var result = new PlayerStatsViewModel
            {
                AsOf = queryDate
            };
            var strDate = UniversalDate(queryDate);
            var qp = new System.Text.StringBuilder();
            qp.Append("ranking=baseball_daily_player_hitter_ranking");
            qp.Append($"&on={strDate}");
            //qp.Append($"&player_id={playerSlug}");
            var httpWebRequest = CreateRankingsRequest(
                queryParms: qp.ToString());

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var json = streamReader.ReadToEnd();
                var dto = JsonConvert.DeserializeObject<PlayerRankingDto>(
                    json);

                Teams = dto.Teams;
                //PlayerStats = dto.PlayerStats;
            }
            result.Player = new Domain.Player
            {
                Name = playerSlug
            };

            return result;
        }
    }
}
