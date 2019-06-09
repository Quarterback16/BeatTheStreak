using BeatTheStreak.Models;
using System;
using System.Collections.Generic;

namespace BeatTheStreak.Interfaces
{
	public interface IHotList
	{
		List<HotListViewModel> GetHotList(
			List<string> teamSlugs,
			DateTime queryDate,
			int gamesBack);
	}
}
