using System;
using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Cache.Interfaces;

namespace BeatTheStreak.Repositories
{
	public class CachedLineupRepository : ILineupRepository
	{
		private readonly ILineupRepository _decoratedComponent;
		private readonly ICacheRepository _cache;

		public CachedLineupRepository(
			ILineupRepository decoratedComponent,
			ICacheRepository cache)
		{
			_decoratedComponent = decoratedComponent;
			_cache = cache;
		}

		public LineupViewModel Submit(DateTime queryDate, string teamSlug)
		{
			var keyValue = $"lineup:{teamSlug}:{Utility.UniversalDate(queryDate)}";
			if (!_cache.TryGet(keyValue, out LineupViewModel viewModel))
			{
				viewModel = _decoratedComponent.Submit(queryDate, teamSlug);
				_cache.Set(keyValue, viewModel);
			}
			return viewModel;
		}
	}
}
