using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Cache.Interfaces;
using System;

namespace BeatTheStreak.Repositories
{
	public class CachedTeamStatsRepository : ITeamStatsRepository
	{
		private readonly ITeamStatsRepository _decoratedComponent;
		private readonly ICacheRepository _cache;

		public CachedTeamStatsRepository(
			ITeamStatsRepository decoratedComponent,
			ICacheRepository cache)
		{
			_decoratedComponent = decoratedComponent;
			_cache = cache;
		}

		public TeamStatsViewModel Submit(DateTime queryDate, string teamSlug)
		{
			var keyValue = $"{teamSlug}:{Utility.UniversalDate(queryDate)}";
			if (!_cache.TryGet(keyValue, out TeamStatsViewModel viewModel))
			{
				viewModel = _decoratedComponent.Submit(queryDate, teamSlug);
				_cache.Set(keyValue, viewModel);
			}
			return viewModel;
		}
	}
}
