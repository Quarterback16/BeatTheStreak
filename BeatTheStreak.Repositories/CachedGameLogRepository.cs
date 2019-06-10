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

		public Result<PlayerGameLogViewModel> Submit(
			DateTime queryDate, 
			string playerSlug)
		{
			var result = Result.Fail<PlayerGameLogViewModel>("empty");
			var keyValue = $@"gamelog:{
				playerSlug
				}:{
				Utility.UniversalDate(queryDate)}";
			if (!_cache.TryGet(
				keyValue,
				out PlayerGameLogViewModel viewModel))
			{
				result = _decoratedComponent.Submit(
					queryDate,
					playerSlug);
				if (result.IsSuccess)
				{
					viewModel = result.Value;
					_cache.Set(keyValue, viewModel);
				}
				else
					return result;
			}
			result = Result.Ok(viewModel);
			return result;
		}
	}
}
