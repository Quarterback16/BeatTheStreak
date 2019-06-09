using BeatTheStreak.Interfaces;
using Domain;
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

		public List<Player> GetActualRoster(
			string teamSlug,
			DateTime queryDate,
			int gamesBack,
			bool battersOnly = false)
		{
			var result = new List<Player>();
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
						{
							Player thisPlayer = CastToPlayer(player);
							AddPlayerIfNew(result, thisPlayer);
						}
					}
					else
					{
						Player thisPlayer = CastToPlayer(player);
						AddPlayerIfNew(result, thisPlayer);
					}
				}
			}
			return result;
		}

		private static Player CastToPlayer(Batter player)
		{
			return new Player
			{
				Name = player.Name,
				Slug = player.PlayerSlug
			};
		}

		private static void AddPlayerIfNew(
			List<Player> players, 
			Player player)
		{
			foreach (var p in players)
			{
				if (p.Name.Equals(player.Name))
					return;
			}

			players.Add(player);
		}

		public List<Player> GetActualRoster(
			List<string> teamSlugs,
			DateTime queryDate, 
			int gamesBack, 
			bool battersOnly = false)
		{
			var result = new List<Player>();
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
