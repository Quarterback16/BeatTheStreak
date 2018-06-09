using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Domain;
using System;

namespace Application
{
	public class ResultChecker
	{
		private readonly IPlayerStatsRepository _playerStatsRepository;

		public ResultChecker(IPlayerStatsRepository playerStatsRepository)
		{
			_playerStatsRepository = playerStatsRepository;
		}

		public bool Check( Batter batter, DateTime gameDate )
		{
			var hit = false;
			if ( gameDate < DateTime.Now.AddDays(-2) )
			{
				var statsAfterGame = _playerStatsRepository.Submit(
					queryDate: gameDate.AddDays(1),
					playerSlug: batter.PlayerSlug);
				var statsBeforeGame = _playerStatsRepository.Submit(
					queryDate: gameDate,
					playerSlug: batter.PlayerSlug);
				if (statsAfterGame.Hits > statsBeforeGame.Hits)
					hit = true;
			}
			else
				Console.WriteLine("Game too fresh");
			return hit;
		}
	}
}
