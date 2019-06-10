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

        public Result<PlayerGameLogViewModel> Submit(
			DateTime queryDate, 
			string playerSlug)
		{
			var result = new PlayerGameLogViewModel
			{
				AsOf = queryDate
			};

			HttpWebRequest httpWebRequest = SetQueryParams(
				queryDate, 
				playerSlug);
			HttpWebResponse httpResponse;
			try
			{
				httpResponse = (HttpWebResponse) httpWebRequest.GetResponse();
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("404"))
				{
					var errMsg = $"Cant find {playerSlug}";
					Console.WriteLine(errMsg);
					return Result.Fail<PlayerGameLogViewModel>(errMsg);
				}
				else
					return Result.Fail<PlayerGameLogViewModel>(
						ex.Message);
			}

			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				var json = streamReader.ReadToEnd();
				var dto = JsonConvert.DeserializeObject<GameLogDto>(json);

				Teams = dto.Teams;
				Logs = dto.Logs;
			}
			if (Logs.Count > 0)
			{
				//  can play a double header
				for (int i = 0; i < Logs.Count; i++)
				{
					if (!EmptyLog(Logs[i]))
						result.HasGame = true;
					MapLogStats(Logs[i], result);
				}
				CalculateAverages(result);
			}

			result.AsOf = queryDate;
			result.Player = new Domain.Player
			{
				Name = playerSlug
			};
			return Result.Ok(result);
		}

		private bool EmptyLog(LogDto log)
		{
			var isEmpty = false;
			if (log.InningsPitched == null
				&& log.AtBats.Equals("0")
				&& log.HitByPitch.Equals("0")
				&& log.IntentionalWalks.Equals("0")
				&& log.SacrificeFlys.Equals("0")
				&& log.SacrificeHits.Equals("0")
				&& log.Walks.Equals("0"))
				isEmpty = true;
			return isEmpty;
		}

		private void CalculateAverages(PlayerGameLogViewModel result)
		{
			result.BattingAverage += Utility.BattingAverage(
				result.Hits, 
				result.AtBats);
			result.OpponentsBattingAverage = Utility.BattingAverage(
				result.HitsAllowed, 
				result.OutsRecorded + result.HitsAllowed);
			result.WOBA = Utility.WOBA(
				walks: result.Walks,
				intentionalWalks: result.IntentionalWalks,
				hitByPitch: result.HitByPitch,
				singles: result.Singles,
				doubles: result.Doubles,
				triples: result.Triples,
				homeRuns: result.HomeRuns,
				atBats: result.AtBats,
				sacrifices: result.Sacrifices);
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

		private void MapLogStats(
			LogDto logDto, 
			PlayerGameLogViewModel result)
		{
			result.IsPitcher = logDto.IsPitcher();
			result.IsBatter = logDto.IsBatter();
			result.GameStarted = Boolean.Parse(logDto.GameStarted);
			result.Hits += SetDecimal(logDto.Hits);
			result.HomeRuns += SetDecimal(logDto.HomeRuns);
			result.Runs += SetDecimal(logDto.Runs);
			result.RunsBattedIn += SetDecimal(logDto.RunsBattedIn);
			result.TotalBases += SetDecimal(logDto.TotalBases);
			result.Walks += SetDecimal(logDto.Walks);
			result.StrikeOuts += SetDecimal(logDto.StrikeOuts);
			result.StolenBases += SetDecimal(logDto.StolenBases);
			result.CaughtStealing += SetDecimal(logDto.CaughtStealing);
			result.AtBats += SetInt(logDto.AtBats);
			result.Era = SetDecimal(logDto.ERA);
			result.EarnedRuns += EarnedRuns(logDto);
			result.HitsAllowed += SetInt(logDto.HitsAllowed);
			result.OutsRecorded += SetInt(logDto.Outs);
			result.BattersStruckOut += SetInt(logDto.BattersStruckOut);
			result.WalksAllowed += SetInt(logDto.WalksAllowed);
			result.InningsPitched += SetDecimal(logDto.InningsPitched);
			result.Wins += SetInt(logDto.Wins);
			result.Losses += SetInt(logDto.Losses);
			result.Saves += SetInt(logDto.Saves);
			result.QualityStarts += SetInt(logDto.QualityStarts);
			result.Whip = SetDecimal(logDto.Whip);
			result.OBP = SetDecimal(logDto.OBP);
			result.OPS = SetDecimal(logDto.OPS);
			result.Singles += SetDecimal(logDto.Singles);
			result.Doubles += SetDecimal(logDto.Doubles);
			result.Triples += SetDecimal(logDto.Triples);
			result.IntentionalWalks += SetDecimal(logDto.IntentionalWalks);
			result.HitByPitch += SetDecimal(logDto.HitByPitch);
			result.Sacrifices += SetDecimal(logDto.SacrificeFlys)
				+ SetDecimal(logDto.SacrificeHits);
		}

		private int EarnedRuns(LogDto logDto)
		{
			if (string.IsNullOrEmpty(logDto.ERA))
				return 0;

			var outs = Outs(logDto.InningsPitched);
			var era = Decimal.Parse(logDto.ERA);
			var erPerOut =  era / 27.0M;
			var er = erPerOut * (decimal) outs;
			return (int) er;
		}

		private int Outs(string inningsPitched)
		{
			if (string.IsNullOrEmpty(inningsPitched))
				return 0;

			decimal ip = Decimal.Parse(inningsPitched);
			var fullInnings = (int) ip;
			var partInnings = ip - (decimal)fullInnings;
			var outs = (int) ( partInnings * 10.0M );
			var totalOuts = (fullInnings * 3) + outs;
			return totalOuts;
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
