using BeatTheStreak.Helpers;
using Domain;
using System;

namespace BeatTheStreak.Models
{
	public class PlayerGameLogViewModel
	{
		public bool HasGame { get; set; }

		public bool IsPitcher { get; set; }
		public bool IsBatter { get; set; }

		public Player Player { get; set; }

		public DateTime AsOf { get; set; }

		public decimal AtBats { get; set; }

		public decimal PlateAppearances { get; set; }

		public decimal Hits { get; set; }

		public decimal HomeRuns { get; set; }

		public decimal Runs { get; set; }

		public decimal RunsBattedIn { get; set; }

		public decimal TotalBases { get; set; }

		public decimal Walks { get; set; }

		public decimal StrikeOuts { get; set; }

		public int BattersStruckOut { get; set; }

		public decimal StolenBases { get; set; }

		public decimal CaughtStealing { get; set; }

		public decimal NetSteals()
		{
			return StolenBases - CaughtStealing;
		}

		public int HitsAllowed { get; set; }
		public int WalksAllowed { get; set; }

		public int EarnedRuns { get; set; }

		public int QualityStarts { get; set; }
		public int Wins { get; set; }
		public int Losses { get; set; }
		public int Saves { get; set; }

		public void Add(PlayerGameLogViewModel log)
		{
			Hits += log.Hits;
			AtBats += log.AtBats;
			Runs += log.Runs;
			HomeRuns += log.HomeRuns;
			TotalBases += log.TotalBases;
			RunsBattedIn += log.RunsBattedIn;
			Walks += log.Walks;
			StrikeOuts += log.StrikeOuts;
			StolenBases += log.StolenBases;
			CaughtStealing += log.CaughtStealing;
			BattingAverage = Utility.BattingAverage( Hits, AtBats);
			InningsPitched = AppInningsPitched(InningsPitched, log.InningsPitched);
			HitsAllowed += log.HitsAllowed;
			EarnedRuns += log.EarnedRuns;
			WalksAllowed += log.WalksAllowed;
			BattersStruckOut += log.BattersStruckOut;
			QualityStarts += log.QualityStarts;
			Wins += log.Wins;
			Losses += log.Losses;
			Saves += log.Saves;
			Whip = Utility.Whip(HitsAllowed, WalksAllowed, InningsPitched);
		}

		private decimal AppInningsPitched(decimal inningsPitched1, decimal inningsPitched2)
		{
			//TODO: 
			return inningsPitched1 + inningsPitched2;
		}

		public decimal BattingAverage { get; set; }

		public decimal Era { get; set; }

		public int OutsRecorded { get; set; }

		public decimal InningsPitched { get; set; }

		public decimal GroundBallTpFlyBallRatio { get; set; }

		public decimal OpponentsBattingAverage { get; set; }

		public decimal Whip { get; set; }

		public bool GameStarted { get; set; }

		public void Dump()
		{
			Console.WriteLine(this);
		}

		public void DumpPitcher()
		{
			Console.WriteLine(PitcherLine());
		}

		public string PitcherLine()
		{
			return $@"{
				Player?.Name
				} on: {
				Utility.UniversalDate(AsOf)
				} W:{
				Wins,-2
				} ERA:{
				Era,-5
				} Outs:{
				OutsRecorded,-4
				} Hits Allowed: {
				HitsAllowed,-5
				} OBA:{OpponentsBattingAverage:#.000}";
		}

		public override string ToString()
		{
			if (IsBatter)
			{
				string bavg;
				if (AtBats > 0)
				{
					//bavg = string.Format("{0:#.000}", Hits / AtBats);  // already calculate
					bavg = string.Format("{0:#.000}", BattingAverage);
				}
				else
					bavg = " .000";
				var ab = (int)AtBats;
				return $@"{
					Player?.Name
					} Asof: {
					AsOf.ToShortDateString(),10
					} {
					Hits,-2
					} for {
					ab,-3
					} {
					bavg,-5
					}";
			}
			else
				return PitcherLine();
		}

		public void BatterLine()
		{
			var line = $@"{
				Player?.Name
				} on {
				AsOf.ToShortDateString()
				} had {AtBats} AB {Hits} hits";
			Console.WriteLine(line);
		}

		public string DateLine( string lineName = "")
		{
			if (string.IsNullOrEmpty(lineName))
			{
				lineName = $"{AsOf:ddd dd MMM}";
			}
			if (!HasGame)
			{
				return $"{lineName,-10}";
			}
			if (IsBatter)
			{
				return BatterLine(lineName);
			}
			return PitcherEspnLine(lineName);
		}

		public string PitcherEspnLine(string lineName = "")
		{
			var line = $@"{lineName,-10}  {PitcherEspnLinePart2()}";
			return line;
		}
		public string PitcherEspnLinePart2()
		{
			var line = $@"{
				InningsPitched,4
				} {
				HitsAllowed,3
				} {
				EarnedRuns,3
				} {
				WalksAllowed,3
				} {
				BattersStruckOut,3
				} {
				QualityStarts,3
				}  {
				Wins,2
				} {
				Losses,2
				} {
				Saves,3
				}  {
				string.Format("{0:#0.000}", Whip),6}";
			return line;
		}

		public string BatterLine(string lineName = "")
		{

			var line = $@"{lineName,-10}  {
				Hits,3
				} {
				AtBats,3
				} {
				Runs,3
				} {
				HomeRuns,3
				} {
				TotalBases,3
				} {
				RunsBattedIn,3
				} {
				Walks,3
				} {
				StrikeOuts,3
				} {
				NetSteals(),3
				}  {
				string.Format("{0:#.000}", BattingAverage)}";
			return line;
		}

		public string DateHeaderLine()
		{
			if (IsBatter)
				return BatterHeaderLine();
			return PitcherHeaderLine();
		}

		public string HeaderLine()
		{
			if (IsBatter)
  			  return LineFor(BatterHeaderLine());
			return LineFor(PitcherHeaderLine());
		}

		private string LineFor(string v)
		{
			return new string('-',v.Length+1);
		}

		public string BatterHeaderLine()
		{
			var line = "Date          H  AB   R  HR  TB  BI  BB   K  SBN  AVG";
			return line;
		}

		public string PitcherHeaderLine()
		{
			var line = "Date          IP   H  ER  BB   K  QS   W  L  SV   WHIP";
			return line;
		}
	}
}
