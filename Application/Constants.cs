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
        }
    }
}
