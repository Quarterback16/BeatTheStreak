using System;
using System.Collections.Generic;

namespace BeatTheStreak.Interfaces
{
	public interface IHotList
	{
		List<string> GetHotList(
			List<string> teamSlugs,
			DateTime queryDate,
			int gamesBack);
	}
}
