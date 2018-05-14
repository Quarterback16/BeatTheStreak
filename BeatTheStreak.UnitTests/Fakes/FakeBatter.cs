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
            TeamName = "Pitsburgh Fakes";
        }

        public FakeBatter(decimal battingAverage)
        {
            Name = "Steve";
            BattingOrder = "1";
            TeamSlug = "mlb-pit";
            PositionAbbreviation = "3B";
            TeamName = "Pitsburgh Fakes";
            BattingAverage = battingAverage;
        }
    }
}
