namespace Application
{
    public static class Constants
    {
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
		}
	}
}
