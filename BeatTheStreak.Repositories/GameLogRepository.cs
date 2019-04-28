using System;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;

namespace BeatTheStreak.Repositories
{
	public class GameLogRepository : IGameLogRepository
	{
		public PlayerGameLogViewModel Submit(
			DateTime queryDate, 
			string playerSlug)
		{
			var request = new GameLogRequest();
			var result = request.Submit(queryDate, playerSlug);
			return result;
		}
	}
}
