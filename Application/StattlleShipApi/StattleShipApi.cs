using System;
using Application.Outputs;

namespace BeatTheStreak
{
    public class StattleShipApi : IStattleShipApi
    {
        public ProbablePitcherViewModel GetProbablePitchers(DateTime queryDate)
        {
            var result = new ProbablePitcherViewModel();
            var request = new ProbablePitcherRequest();
            result = request.Submit(queryDate);
            return result;
        }
    }
}
