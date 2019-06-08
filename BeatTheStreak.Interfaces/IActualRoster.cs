using System;
using System.Collections.Generic;

namespace BeatTheStreak.Interfaces
{
	public interface IActualRoster
	{
		List<string> GetActualRoster(
			string teamSlug,
			DateTime queryDate,
			int gamesBack,
			bool battersOnly = false);

		List<string> GetActualRoster(
			List<string> teamSlugs,
			DateTime queryDate,
			int gamesBack,
			bool battersOnly = false);
	}
}
