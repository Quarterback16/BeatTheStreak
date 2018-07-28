using System;

namespace BeatTheStreak.Models
{
	public class TeamStatsViewModel
	{
		public DateTime AsOf { get; set; }
		public string TeamName { get; set; }
		public int Wins { get; set; }
		public int Losses { get; set; }

		public decimal Clip()
		{
			decimal totalGames = Wins + Losses;
			if (totalGames == 0) return 0.000M;
			var clip = Wins / totalGames;
			clip = Math.Truncate(clip * 1000m) / 1000m;
			return clip;
		}
	}
}
