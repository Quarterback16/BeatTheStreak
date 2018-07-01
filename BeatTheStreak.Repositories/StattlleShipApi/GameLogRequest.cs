using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace BeatTheStreak.Repositories
{
    public class GameLogRequest : BaseApiRequest, IGameLogRequest
    {
        public List<LogDto> Logs { get; set; }

        public PlayerGameLogViewModel Submit(DateTime queryDate, string playerSlug)
        {
            var result = new PlayerGameLogViewModel
            {
                AsOf = queryDate
            };
            var strDate = Utility.UniversalDate(queryDate);
            var qp = new StringBuilder();
            qp.Append("season_id=mlb-2018");
			qp.Append("&interval_type=regularseason");
			qp.Append("&status=ended");
            qp.Append($"&player_id={playerSlug}");
			qp.Append($"&on={strDate}");
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
			if (Logs.Count==1)
			{
				MapLogStats(Logs[0], result);
			}
			result.AsOf = queryDate;
			result.Player = new Domain.Player
            {
                Name = playerSlug
            };

            return result;
        }

		private void MapLogStats(LogDto logDto, PlayerGameLogViewModel result)
		{
			result.GameStarted = Boolean.Parse(logDto.GameStarted);
			result.Hits = SetDecimal(logDto.Hits);
			result.AtBats = SetInt(logDto.AtBats);
			result.BattingAverage = Utility.BattingAverage(result.Hits, result.AtBats);
			result.Era = SetDecimal(logDto.ERA);
			result.HitsAllowed = SetInt(logDto.HitsAllowed);
			result.OutsRecorded = SetInt(logDto.Outs);
			result.OpponentsBattingAverage = Utility.BattingAverage(
				result.HitsAllowed, result.OutsRecorded + result.HitsAllowed);
		}

		private decimal SetDecimal(string amount)
		{
			if (string.IsNullOrEmpty(amount))
				return 0.0M;
	
			return Decimal.Parse(amount);
		}

		private int SetInt(string amount)
		{
			if (string.IsNullOrEmpty(amount))
				return 0;

			return Int32.Parse(amount);
		}
	}
}
