using BeatTheStreak.Models;
using Domain;
using System;

namespace BeatTheStreak.Interfaces
{
	public interface ILineupProjector
	{
		LineupViewModel ProjectLineup(
			Pitcher pitcher,
			DateTime lineupQueryDate);
	}
}
