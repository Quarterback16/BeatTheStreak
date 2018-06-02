using BeatTheStreak.Models;
using System;

namespace BeatTheStreak.Interfaces
{
    public interface IPlayerStatsRepository
    {
        PlayerStatsViewModel Submit(DateTime queryDate, string playerSlug);
    }
}
