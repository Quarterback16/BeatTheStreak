using System;
using System.Collections.Generic;
using Domain;

namespace BeatTheStreak
{
    public class StattleShipApi : IStattleShipApi
    {
        public List<Pitcher> GetProbablePitchers(DateTime queryDate)
        {
            var result = new List<Pitcher>();
            // build request
            var request = new ProbablePitcherRequest();

            // submit request
            result = request.Submit(queryDate);
            // parse results
            return result;
        }
    }
}
