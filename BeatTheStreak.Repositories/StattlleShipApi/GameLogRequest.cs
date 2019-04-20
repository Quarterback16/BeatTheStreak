﻿using BeatTheStreak.Helpers;
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

			HttpWebRequest httpWebRequest = SetQueryParams(queryDate, playerSlug);
			HttpWebResponse httpResponse;
			try
			{
				httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("404"))
				{
					Console.WriteLine($"Cant find {playerSlug}");
					return result;
				}
				else
					throw;
			}

			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				var json = streamReader.ReadToEnd();
				var dto = JsonConvert.DeserializeObject<GameLogDto>(json);

				Teams = dto.Teams;
				Logs = dto.Logs;
			}
			if (Logs.Count == 1)
			{
				MapLogStats(Logs[0], result);
				result.HasGame = true;
			}

			result.AsOf = queryDate;
			result.Player = new Domain.Player
			{
				Name = playerSlug
			};
			return result;
		}

		private HttpWebRequest SetQueryParams(
			DateTime queryDate, 
			string playerSlug)
		{
			var strDate = Utility.UniversalDate(queryDate);
			var qp = new StringBuilder();
			qp.Append($"season_id=mlb-{queryDate.Year}");
			qp.Append("&interval_type=regularseason");
			qp.Append("&status=ended");
			qp.Append($"&player_id={playerSlug}");
			qp.Append($"&on={strDate}");
			var httpWebRequest = CreateRequest(
				sport: "baseball",
				league: "mlb",
				apiRequest: "game_logs",
				queryParms: qp.ToString());
			return httpWebRequest;
		}

		private void MapLogStats(LogDto logDto, PlayerGameLogViewModel result)
		{
			result.IsPitcher = logDto.IsPitcher();
			result.IsBatter = logDto.IsBatter();
			result.GameStarted = Boolean.Parse(logDto.GameStarted);
			result.Hits = SetDecimal(logDto.Hits);
			result.HomeRuns = SetDecimal(logDto.HomeRuns);
			result.Runs = SetDecimal(logDto.Runs);
			result.RunsBattedIn = SetDecimal(logDto.RunsBattedIn);
			result.TotalBases = SetDecimal(logDto.TotalBases);
			result.Walks = SetDecimal(logDto.Walks);
			result.StrikeOuts = SetDecimal(logDto.StrikeOuts);
			result.StolenBases = SetDecimal(logDto.StolenBases);
			result.CaughtStealing = SetDecimal(logDto.CaughtStealing);
			result.AtBats = SetInt(logDto.AtBats);
			result.BattingAverage = Utility.BattingAverage(result.Hits, result.AtBats);
			result.Era = SetDecimal(logDto.ERA);
			result.EarnedRuns = SetInt(logDto.EarnedRuns);
			result.HitsAllowed = SetInt(logDto.HitsAllowed);
			result.OutsRecorded = SetInt(logDto.Outs);
			result.BattersStruckOut = SetInt(logDto.BattersStruckOut);
			result.WalksAllowed = SetInt(logDto.WalksAllowed);
			result.InningsPitched = SetDecimal(logDto.InningsPitched);
			result.Wins = SetInt(logDto.Wins);
			result.Losses = SetInt(logDto.Losses);
			result.Saves = SetInt(logDto.Saves);
			result.QualityStarts = SetInt(logDto.QualityStarts);
			result.Whip = SetDecimal(logDto.Whip);
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

			if (amount.Equals("0.0"))
				amount = "0";

			return Int32.Parse(amount);
		}
	}
}
