using BeatTheStreak.Models;
using System;

namespace BeatTheStreak.Interfaces
{
    public interface IStattleShipApi
    {
        ProbablePitcherViewModel GetProbablePitchers(DateTime queryDate);
    }
}