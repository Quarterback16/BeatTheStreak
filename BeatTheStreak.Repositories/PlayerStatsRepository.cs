using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using System;

namespace BeatTheStreak.Repositories
{
    public class PlayerStatsRepository : IPlayerStatsRepository
    {
        public PlayerStatsViewModel Submit(DateTime queryDate, string playerSlug)
        {
			var statsRequest = new PlayerStatsRequest();
			var result = statsRequest.Submit(queryDate,playerSlug);
			return result;
		}
    }
}
