using System;

namespace BeatTheStreak.Helpers
{
    public static class Utility
    {
		public static string UniversalDate(DateTime date)
		{
			var longDate = $"{date.Date:u}";
			var universalDate = longDate.Substring(0, 10);
			var year = universalDate.Substring(0, 5);
			var month = universalDate.Substring(5, 2);
			var day = universalDate.Substring(8, 2);
			var usUniversalDate = $"{year}-{day}-{month}";
			return universalDate;
		}

		public static decimal BattingAverage(
			decimal hits,
			decimal atBats)
		{
			if (atBats == 0) return 0.0M;
			return hits / atBats;
		}
	}
}
