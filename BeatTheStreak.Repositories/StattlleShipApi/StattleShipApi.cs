using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using System;

namespace BeatTheStreak.Repositories
{
    public class StattleShipApi : IStattleShipApi
    {
        public ProbablePitcherViewModel GetProbablePitchers(DateTime queryDate)
        {
            var result = new ProbablePitcherViewModel();
            var request = new ProbablePitcherRequest(null);
            result = request.Submit(queryDate);
            return result;
        }
    }
}
