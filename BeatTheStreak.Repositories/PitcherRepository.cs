using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using System;

namespace BeatTheStreak.Repositories
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
