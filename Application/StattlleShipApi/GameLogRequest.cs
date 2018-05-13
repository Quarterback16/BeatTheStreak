﻿using Application.Outputs;
using Application.StattlleShipApi.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Application.StattlleShipApi
{
    public class GameLogRequest : BaseApiRequest
    {
        public List<LogDto> Logs { get; set; }

        public PlayerStatsViewModel Submit(DateTime queryDate, string playerSlug)
        {
            var result = new PlayerStatsViewModel
            {
                AsOf = queryDate
            };
            var strDate = UniversalDate(queryDate);
            var qp = new StringBuilder();
            //qp.Append($"season_id=mlb-2018");
            //qp.Append($"&on={strDate}");
            //qp.Append($"&status=ended");
            //qp.Append($"&team_id=mlb-pit");
            //qp.Append($"&player_id={playerSlug}");
            var httpWebRequest = CreateRequest(
                sport: "baseball",
                league: "mlb",
                apiRequest: "game_logs",
                queryParms: qp.ToString());

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var json = streamReader.ReadToEnd();
                var dto = JsonConvert.DeserializeObject<GameLogDto>(json);

                Teams = dto.Teams;
                Logs = dto.Logs;
            }
            result.Player = new Domain.Player
            {
                Name = playerSlug
            };

            return result;
        }
    }
}
