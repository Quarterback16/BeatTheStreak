using System;
using System.Collections.Generic;
using Application.Outputs;
using BeatTheStreak;
using Domain;

namespace Application.Repositories
{
    public class PitcherRepository : IPitcherRepository
    {
        public ProbablePitcherViewModel Submit(DateTime gameDate)
        {
            var pitcherRequest = new ProbablePitcherRequest();
            var pitchers = pitcherRequest.Submit(gameDate);
            return pitchers;
        }
    }
}
