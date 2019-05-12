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

		public static decimal StrikeOutRate(
			decimal strikeouts,
			decimal atBats)
		{
			if (atBats == 0) return 0.0M;
			var avg = strikeouts / atBats;
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
			if (playerName.Equals("Cody Bellinger"))
				return "cody-bellinger";
			if (playerName.Equals("Franmil Reyes"))
				return "mlb-04fea9c7-bf92-44e5-9589-58ecd343cc61";
			if (playerName.Equals("Dwight Smith"))
				return "mlb-john-smith-jr";
			if (playerName.Equals("Mitch Garver"))
				return "mitchell-garver";
			if (playerName.Equals("Hunter Dozier"))
				return "hunter-dozier";
			if (playerName.Equals("Dansby Swanson"))
				return "dansby-swanson";
			if (playerName.Equals("Juan Soto"))
				return "mlb-245cb216-9fdc-4814-b3c8-b7982c23c082";
			if (playerName.Equals("Nicholas Castellanos"))
				return "mlb-nick-castellanos";
			if (playerName.Equals("Christopher Paddack"))
				return "mlb-chris-paddack-1996-01-08";
			if (playerName.Equals("Bradley Peacock"))
				return "mlb-brad-peacock";
			if (playerName.Equals("Niko Goodrum"))
				return "mlb-cartier-goodrum";

			return $"mlb-{playerName.Replace(' ', '-').ToLower()}";
		}

		public static DateTime WeekStart( int weekNo )
		{
			return new DateTime(2019, 4, 1).AddDays((weekNo-1)*7);
		}

		public static decimal WOBA(
			decimal walks, 
			decimal intentionalWalks, 
			decimal hitByPitch, 
			decimal singles, 
			decimal doubles, 
			decimal triples, 
			decimal homeRuns, 
			decimal atBats, 
			decimal sacrifices)
		{
			if (atBats == 0)
				return 0.0M;
			decimal divisor = atBats
				+ walks
				- intentionalWalks
				+ sacrifices
				+ hitByPitch;
			decimal quotient = ((walks - intentionalWalks) * .69M)
				+ hitByPitch * .72M
				+ singles * .876M
				+ doubles * 1.236M
				+ triples * 1.56M
				+ homeRuns * 1.995M;
			return quotient / divisor;
		}

		public static decimal FIP(
			decimal homeRunsAllowed,
			decimal strikeOuts,
			decimal walksAllowed,
			decimal battersHitByPitch,
			decimal inningsPitched )
		{
			if (inningsPitched == 0.0M)
				return 0.0M;
			decimal quotient = homeRunsAllowed * 13
				+ (walksAllowed + battersHitByPitch) * 3
				- strikeOuts * 2;
			decimal divisor = FixIp(inningsPitched);
			return Math.Round(
				d: ( quotient / divisor ) + 3.161M,
				decimals: 2 );
		}

		public static decimal FixIp(decimal inningsPitched)
		{
			var ipToString = inningsPitched.ToString();
			string[] strArr = ipToString.Split('.');
			
			if (strArr[1] == "1")
				return Decimal.Parse(strArr[0]+".333");
			if (strArr[1] == "2")
				return Decimal.Parse(strArr[0] + ".666");
			return inningsPitched;
		}
	}
}
