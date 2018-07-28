using BeatTheStreak.Models;
using System;

namespace BeatTheStreak.Interfaces
{
	public interface ITeamStatsRepository
	{
		TeamStatsViewModel Submit(DateTime queryDate, string teamSlug);
	}
}
