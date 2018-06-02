using Domain;

namespace BeatTheStreak.UnitTests.Fakes
{
    public class FakeBatter : Batter
    {
        public FakeBatter()
        {
            Name = "Steve";
            BattingOrder = "1";
            TeamSlug = "mlb-pit";
            PositionAbbreviation = "3B";
            TeamName = "Pittsburgh Fakes";
			PlayerSlug = "mlb-test-player";
        }

        public FakeBatter(decimal battingAverage) : this()
        {
            BattingAverage = battingAverage;
		}
    }
}
