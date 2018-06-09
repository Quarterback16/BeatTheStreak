using Domain;
using System;

namespace BeatTheStreak.Models
{
    public class PlayerStatsViewModel
    {
		public DateTime AsOf { get; set; }

        public Player Player { get; set; }

        public decimal AtBats { get; set; }

        public decimal PlateAppearances { get; set; }

        public decimal Hits { get; set; }

		public int HitsAllowed { get; set; }

		public decimal BattingAverage { get; set; }

        public decimal Era { get; set; }

        public int Wins { get; set; }

        public decimal InningsPitched { get; set; }

        public decimal GroundBallTpFlyBallRatio { get; set; }

        public decimal OpponentsBattingAverage { get; set; }

        public decimal Whip { get; set; }

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
				} Asof: {
				AsOf.ToShortDateString()
				} W:{
				Wins,-2
				} ERA:{
				Era,-5
				} IP:{
				InningsPitched,-4
				} Hits Allowed: {
				HitsAllowed,-5
				} GBR:{ GroundBallTpFlyBallRatio:#.000} OBA:{OpponentsBattingAverage:#.000}";
        }

        public override string ToString()
        {
            string bavg;
            if (AtBats > 0)
            {
                //bavg = string.Format("{0:#.000}", Hits / AtBats);  // already calculate
                bavg = string.Format("{0:#.000}", BattingAverage );
            }
            else
                bavg = " .000";
            var ab = (int) AtBats;
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
    }
}
