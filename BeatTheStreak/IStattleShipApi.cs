using Domain;
using System;
using System.Collections.Generic;

namespace BeatTheStreak
{
    public interface IStattleShipApi
    {
       List<Pitcher> GetProbablePitchers(DateTime queryDate);
    }
}