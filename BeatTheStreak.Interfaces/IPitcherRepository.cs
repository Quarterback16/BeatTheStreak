using BeatTheStreak.Models;
using System;

namespace BeatTheStreak.Interfaces
{
    public interface IPitcherRepository
    {
        ProbablePitcherViewModel Submit(
            DateTime gameDate, 
            bool homeOnly = false);
    }
}
