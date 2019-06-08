using BeatTheStreak.Models;
using System;

namespace BeatTheStreak.Interfaces
{
    public interface ILineupRepository
    {
        LineupViewModel Submit(
			DateTime queryDate, 
			string teamSlug);
    }
}
