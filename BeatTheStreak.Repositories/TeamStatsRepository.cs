using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using System;

namespace BeatTheStreak.Repositories
{
	public class TeamStatsRepository : ITeamStatsRepository
	{
		public TeamStatsViewModel Submit(DateTime queryDate, string teamSlug)
		{
			var statsRequest = new TeamStatsRequest();
			var result = statsRequest.Submit(queryDate, teamSlug);
			return result;
		}
	}
}
