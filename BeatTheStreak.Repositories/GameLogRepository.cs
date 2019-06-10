using System;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using BeatTheStreak.Helpers;

namespace BeatTheStreak.Repositories
{
	public class GameLogRepository : IGameLogRepository
	{
		public Result<PlayerGameLogViewModel> Submit(
			DateTime queryDate, 
			string playerSlug)
		{
			var request = new GameLogRequest();
			var result = request.Submit(queryDate, playerSlug);
			return result;
		}
	}
}
