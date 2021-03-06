﻿namespace Domain
{
    public class Pitcher
    {
        public string Name { get; set; }

		public string Slug { get; set; }

		public string Throws { get; set; }

		public int Wins { get; set; }

        public int Losses { get; set; }

        public decimal Era { get; set; }

        public decimal OpponentsBattingAverage { get; set; }

        public string PlayerId { get; set; }

        public string TeamSlug{ get; set; }

		public string TeamId { get; set; }

		public string TeamName { get; set; }

        public string NextOpponent { get; set; }

        public string OpponentId { get; set; }

        public string OpponentSlug { get; set; }

        public bool Away { get; set; }

		public string FantasyTeam { get; set; }

		public override string ToString()
        {
            var oba = string.Format("{0:#0.000}", OpponentsBattingAverage);
            var era = string.Format("{0:#0.00}", Era);
            return $@"{Name,-20} {
				Throws
				}  {TeamName,-15} {FantasyTeam,4} ({Wins,2}-{Losses,2}) {oba,5} {era,5} {
				Slug
				}";
        }
    }
}
