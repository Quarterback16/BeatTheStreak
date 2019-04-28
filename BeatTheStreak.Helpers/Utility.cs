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
			var avg = hits / atBats;
			avg = Math.Truncate(avg * 1000m) / 1000m;
			return avg;
		}

		public static decimal Whip(
			int hitsAllowed,
			int walksAllowed,
			decimal inningsPitched)
		{
			if (inningsPitched == 0.0M)
				return inningsPitched;
			return (hitsAllowed + walksAllowed) / inningsPitched;
		}

		public static bool GamePlayed(DateTime gameDate)
		{
			if (DateTime.Now > gameDate.AddDays(1))
			{
				return true;
			}
			return false;
		}

		public static string PlayerSlug(string playerName)
		{
			if (playerName.Equals("Paul DeJong"))
				return "paul-dejong";  //  no prefix for some reason
			if (playerName.Equals("Ryon Healy"))
				return "ryon-healy";
			if (playerName.Equals("Luke Weaver"))
				return "luke-weaver";
			if (playerName.Equals("Maxwell Scherzer"))
				return "mlb-max-scherzer";
			if (playerName.Equals("Robert Ray"))
				return "mlb-robbie-ray";
			if (playerName.Equals("Michael Fiers"))
				return "mlb-mike-fiers";
			if (playerName.Equals("Alexander Cobb"))
				return "mlb-alex-cobb";
			if (playerName.Equals("Carlos Rodón"))
				return "mlb-carlos-rodon";
			if (playerName.Equals("Daniel Duffy"))
				return "mlb-danny-duffy";

			return $"mlb-{playerName.Replace(' ', '-').ToLower()}";
		}

		public static DateTime WeekStart( int weekNo )
		{
			return new DateTime(2019, 4, 1).AddDays((weekNo-1)*7);
		}
	}
}
