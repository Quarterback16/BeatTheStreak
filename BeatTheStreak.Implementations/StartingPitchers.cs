using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Domain;
using System;

namespace BeatTheStreak.Implementations
{
	public class StartingPitchers : IStartingPitcher
	{
		private readonly IStartingPitchersRepository _startersRepository;
		private readonly IGameLogRepository _gameLogRepository;
		private readonly ILog _logger;

		public StartingPitchers(
			IStartingPitchersRepository startersRepository,
			IGameLogRepository gameLogRepository,
			ILog logger)
		{
			_startersRepository = startersRepository;
			_gameLogRepository = gameLogRepository;
			_logger = logger;
		}

		public StartersViewModel PitcherReport(
			DateTime gameDate)
		{
			var result = _startersRepository.Submit(gameDate);
			foreach (var sp in result.Pitchers)
			{
				var gl = _gameLogRepository.Submit(
					gameDate,
					sp.Slug	);
				result.Add(gl, sp.Slug);
			}
			return result;
		}

		public StartersViewModel HotList(
			int weekNo)
		{
			var hotties = new StartersViewModel();

			var weekStarts = Utility.WeekStart(weekNo);
			hotties.StartDate = weekStarts;

			for (int d = 0; d < 7; d++)
			{
				var queryDate = weekStarts.AddDays(d);
				if (queryDate.Equals(DateTime.Now.Date.AddDays(-1)))
					break;
				hotties.EndDate = queryDate;
				var result = _startersRepository.Submit(queryDate);
				foreach (var sp in result.Pitchers)
				{
					var gl = _gameLogRepository.Submit(
						queryDate,
						sp.Slug);
					result.Add(gl, sp.Slug);
					if (IsHottie(sp, gl))
						hotties.Add(sp, gl);
				}
			}

			return hotties;
		}

		private bool IsHottie(Pitcher sp, PlayerGameLogViewModel gl)
		{
			if (sp.FantasyTeam.Equals("FA")
				&& gl.EarnedRuns.Equals(0))
				return true;
			return false;
		}

		public Pitcher Starter(string teamSlug, DateTime gameDate)
		{
			//  TODO: This single pitcher query may not be required
			//        Really more interested in a report on a day
			var starter = new Pitcher();
			// start with the starters
			var lineup = _startersRepository.Submit(
				gameDate);
			return starter;
		}
	}
}
