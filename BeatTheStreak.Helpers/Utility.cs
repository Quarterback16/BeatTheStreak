using System;
using System.IO;

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

		public static decimal WalkRate(
			decimal walks,
			decimal atBats)
		{
			if (atBats == 0) return 0.0M;
			var avg = walks / atBats;
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
			if (playerName.Equals("Ronny Rodriguez"))
				return "ronny-rodriguez";
			if (playerName.Equals("Brendan Rodgers"))
				return "mlb-441341d5-e864-421f-860c-abc2df728b8f";
			if (playerName.Equals("Ronald Acuna Jr."))
				return "mlb-90806bda-a8be-4b6b-902e-25a0e1dda47a";
			if (playerName.Equals("Shohei Ohtani"))
				return "mlb-2b02b25f-ca1e-48f2-9043-09243be07c60";
			if (playerName.Equals("Shohei Ohtani"))
				return "mlb-d23115ea-11b3-40d3-9a07-eeb58d052097";
			if (playerName.Equals("Corbin Martin"))
				return "mlb-e7a12cf4-b89a-498c-b8d3-efbabf6654ca";
			if (playerName.Equals("Shaun Anderson"))
				return "mlb-d6cd8581-8619-4279-887f-90472191046d";
			if (playerName.Equals("Austin Riley"))
				return "mlb-fa7d06c8-a047-45a9-9f0b-874ad3bc6d21";
			if (playerName.Equals("Alex Verdugo"))
				return "alex-verdugo";
			if (playerName.Equals("Clint Frazier"))
				return "clint-frazier";
			if (playerName.Equals("Trey Mancini"))
				return "trey-mancini";
			if (playerName.Equals("Christopher Cron"))
				return "mlb-c-j-cron";
			if (playerName.Equals("Devin Smeltzer"))
				return "mlb-adb72afe-8a88-4cb0-991d-185e3c596718";
			if (playerName.Equals("Shane Bieber"))
				return "mlb-52cf752a-7dbf-4d92-a15b-01488acd3abd";
			if (playerName.Equals("Welington Castillo"))
				return "mlb-welington-castillo";
			if (playerName.Equals("Griffin Canning"))
				return "mlb-5cfeb235-4f55-479b-9643-abaf676e4167";
			if (playerName.Equals("Felix Pena"))
				return "felix-pena";
			if (playerName.Equals("Trevor Richards"))
				return "mlb-201ad9a5-9194-46b6-aac0-ee8080dbb4ee";
			if (playerName.Equals("Brian Reynolds"))
				return "mlb-28968740-3ac9-4ed2-bd6f-f36829d8090a";
			if (playerName.Equals("Yordan Alvarez"))
				return "mlb-0ec6c395-d965-455f-9260-8e357ca5355c";
			if (playerName.Equals("Michael Moustakas"))
				return "mlb-mike-moustakas";
			if (playerName.Equals("Frederick Freeman"))
				return "mlb-freddie-freeman";
			if (playerName.Equals("David LeMahieu"))
				return "mlb-dj-lemahieu";
			if (playerName.Equals("David LeMahieu"))
				return "mlb-dj-lemahieu";
			if (playerName.Equals("Scott Kingery"))
				return "mlb-4c20774e-c58d-4b6e-9b6d-c84fd50e188a";

			return $"mlb-{playerName.Replace(' ', '-').ToLower()}";
		}

		public static int CurrentWeek()
		{
			return 12;
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
			if (divisor == 0.0M)
				return 0.0M;

			decimal quotient = ((walks - intentionalWalks) * .69M)
				+ hitByPitch * .72M
				+ singles * .876M
				+ doubles * 1.236M
				+ triples * 1.56M
				+ homeRuns * 1.995M;
			return Math.Round(quotient / divisor, 3);
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
			decimal quotient = ( homeRunsAllowed * 13 )
				+ ((walksAllowed + battersHitByPitch) * 3)
				- (strikeOuts * 2);
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

		public static bool ItsBeforeFour()
		{
			var hr = DateTime.Now.Hour;  // 0 to 23
			return (hr < 16);
		}

		public static void EnsureDirectory(string destFile)
		{
			var directoryInfo = new FileInfo(destFile).Directory;
			if (directoryInfo != null)
			{
				if (!Directory.Exists(directoryInfo.ToString()))
				{
					Directory.CreateDirectory(
						path: directoryInfo.ToString());
				}
			}
		}
	}
}
