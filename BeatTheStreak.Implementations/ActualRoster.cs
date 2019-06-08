using BeatTheStreak.Interfaces;
using System;
using System.Collections.Generic;

namespace BeatTheStreak.Implementations
{
	public class ActualRoster : IActualRoster
	{
		private readonly ILineupRepository _lineupRepo;

		public ActualRoster(
			ILineupRepository lineupRepo)
		{
			_lineupRepo = lineupRepo;
		}

		public List<string> GetActualRoster(
			string teamSlug,
			DateTime queryDate,
			int gamesBack,
			bool battersOnly = false)
		{
			var result = new List<string>();
			for (int d = 0; d < gamesBack; d++)
			{
				var lineupDate = queryDate.AddDays(-d);
				var lineup = _lineupRepo.Submit(
					lineupDate,
					teamSlug);
				foreach (var player in lineup.Lineup)
				{
					if (battersOnly)
					{
						if (player.IsBatter())
							AddPlayerIfNew(result, player);
					}
					else
						AddPlayerIfNew(result, player);
				}
			}
			return result;
		}

		private static void AddPlayerIfNew(
			List<string> result, 
			Domain.Batter player)
		{
			if (result.Contains(player.Name))
				return;

			result.Add(player.Name);
		}

		public List<string> GetActualRoster(
			List<string> teamSlugs,
			DateTime queryDate, 
			int gamesBack, 
			bool battersOnly = false)
		{
			var result = new List<string>();
			foreach (var team in teamSlugs)
			{
				result.AddRange(
					GetActualRoster(
						team,
						queryDate,
						gamesBack,
						battersOnly));
			}
			return result;
		}
	}
}
