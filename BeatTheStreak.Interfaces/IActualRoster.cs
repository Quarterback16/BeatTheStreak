using Domain;
using System;
using System.Collections.Generic;

namespace BeatTheStreak.Interfaces
{
	public interface IActualRoster
	{
		List<Player> GetActualRoster(
			string teamSlug,
			DateTime queryDate,
			int gamesBack,
			bool battersOnly = false);

		List<Player> GetActualRoster(
			List<string> teamSlugs,
			DateTime queryDate,
			int gamesBack,
			bool battersOnly = false);
	}
}
