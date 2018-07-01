using System;
using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Cache.Interfaces;

namespace BeatTheStreak.Repositories
{
	public class CachedGameLogRepository : IGameLogRepository
	{
		private readonly IGameLogRepository _decoratedComponent;
		private readonly ICacheRepository _cache;

		public CachedGameLogRepository(
			IGameLogRepository decoratedComponent,
			ICacheRepository cache)
		{
			_decoratedComponent = decoratedComponent;
			_cache = cache;
		}

		public PlayerGameLogViewModel Submit(
			DateTime queryDate, 
			string playerSlug)
		{
			var keyValue = $@"gamelog:{
				playerSlug
				}:{
				Utility.UniversalDate(queryDate)}";
			if (!_cache.TryGet(keyValue, out PlayerGameLogViewModel viewModel))
			{
				viewModel = _decoratedComponent.Submit(queryDate, playerSlug);
				_cache.Set(keyValue, viewModel);
			}
			return viewModel;
		}
	}
}
