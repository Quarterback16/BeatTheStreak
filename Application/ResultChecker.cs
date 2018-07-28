using BeatTheStreak.Interfaces;
using Domain;
using System;

namespace Application
{
	public class ResultChecker : IResultChecker
	{
		private readonly IPlayerStatsRepository _playerStatsRepository;

		public ResultChecker(IPlayerStatsRepository playerStatsRepository)
		{
			_playerStatsRepository = playerStatsRepository;
		}

		public bool GotHit( Batter batter, DateTime gameDate )
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

		public bool HadAtBat(Batter batter, DateTime gameDate)
		{
			var hadAtBat = false;
			if (gameDate < DateTime.Now.AddDays(-2))
			{
				if (batter == null || batter.PlayerSlug == null) return false;
				var statsAfterGame = _playerStatsRepository.Submit(
					queryDate: gameDate.AddDays(1),
					playerSlug: batter.PlayerSlug);
				var statsBeforeGame = _playerStatsRepository.Submit(
					queryDate: gameDate,
					playerSlug: batter.PlayerSlug);
				if (statsAfterGame.AtBats > statsBeforeGame.AtBats)
					hadAtBat = true;
			}
			else
				Console.WriteLine("Game too fresh");
			return hadAtBat;
		}

		public string Result(Batter batter, DateTime gameDate)
		{
			var result = "     ";
			if (HadAtBat(batter, gameDate))
			{
				if (GotHit(batter, gameDate))
					result = "HIT";
				else
					result = "OUT";
			}
			else
				result = "DNP";
			return result;
		}
	}
}
