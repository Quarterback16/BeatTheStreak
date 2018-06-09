using System;
using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Cache.Interfaces;

namespace BeatTheStreak.Repositories
{
	public class CachedPlayerStatsRepository : IPlayerStatsRepository
	{
		private readonly IPlayerStatsRepository _decoratedComponent;
		private readonly ICacheRepository _cache;

		public CachedPlayerStatsRepository(
			IPlayerStatsRepository decoratedComponent,
			ICacheRepository cache )
		{
			_decoratedComponent = decoratedComponent;
			_cache = cache;
		}

		public PlayerStatsViewModel Submit(DateTime queryDate, string playerSlug)
		{
			var keyValue = $"{playerSlug}:{Utility.UniversalDate(queryDate)}";
			if (!_cache.TryGet(keyValue, out PlayerStatsViewModel viewModel ))
			{
				viewModel = _decoratedComponent.Submit(queryDate, playerSlug);
				_cache.Set(keyValue, viewModel);
			}
			return viewModel;
		}
	}
}
