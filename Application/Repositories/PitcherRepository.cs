using System;
using System.Collections.Generic;
using BeatTheStreak;
using Domain;

namespace Application.Repositories
{
    public class PitcherRepository : IPitcherRepository
    {
        public List<Pitcher> Submit(DateTime gameDate)
        {
            var pitcherRequest = new ProbablePitcherRequest();
            var pitchers = pitcherRequest.Submit(gameDate);
            return pitchers;
        }
    }
}
