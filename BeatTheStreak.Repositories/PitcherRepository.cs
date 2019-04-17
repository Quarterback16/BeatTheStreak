using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using FbbEventStore;
using System;

namespace BeatTheStreak.Repositories
{
    public class PitcherRepository : IPitcherRepository
    {
        public ProbablePitcherViewModel Submit(
            DateTime gameDate,
            bool homeOnly = false)
        {
			var es = new FbbEventStore.FbbEventStore();
			var rm = new FbbRosters(es);
			var pitcherRequest = new ProbablePitcherRequest(rm,homeOnly);
            var pitchers = pitcherRequest.Submit(gameDate);
            return pitchers;
        }
    }
}
