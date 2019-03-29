namespace Application
{
    public static class Constants
    {
		public static class Mlb
		{
			public const string CurrentSeason = "2019";
		}

        public static class Options
        {
            //  this option is ON will filter out away pitchers
            public const string HomePitchersOnly = "homePitchers";

            //  this option is ON will filter out batters who
            //  have not played in the last x days
            public const string NoDaysOff = "noDaysOff";
            //  this is how many days back we go to check for days off
            public const string DaysOffDaysBack = "daysOffDaysBack";

			//  this option is to filter only recently hot batters
			public const string HotBatters = "HotBatters";
			//  how far back to go for stats
			public const string HotBattersDaysBack = "HotBatter-DaysBack";
			public const string HotBattersMendozaLine = "HotBatters-MendozaLine";
			//  dont touch these pitchers
			public const string PitchersMendozaLine = "Pitchers-MendozaLine";

			public const string PitcherDaysBack = "Pitchers-Days-Back";

			// number of Batters to pick
			public const string BattersToPick = "Batters-to-pick";

			//  Analyse the Team Clip
			public const string TeamClip = "Use-Team-Clips";

			//  dont challenge this team
			public const string PitchersTeamMendozaLine = "Pitchers-Team-MendozaLine";

			//  dont trust teams below this line
			public const string BattersTeamMendozaLine = "Batters-Team-MendozaLine";

			//  this is how many lineup positions to include
			public const string LineupPositions = "Lineup-Positions-To-Examine";
		}
	}
}
