using System;
using Application.Outputs;
using BeatTheStreak;

namespace Application.Repositories
{
    public class PitcherRepository : IPitcherRepository
    {
        public ProbablePitcherViewModel Submit(
            DateTime gameDate,
            bool homeOnly = false)
        {
            var pitcherRequest = new ProbablePitcherRequest(homeOnly);
            var pitchers = pitcherRequest.Submit(gameDate);
            return pitchers;
        }
    }
}
