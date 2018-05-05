using Application.Outputs;
using System;

namespace BeatTheStreak
{
    public interface IStattleShipApi
    {
        ProbablePitcherViewModel GetProbablePitchers(DateTime queryDate);
    }
}