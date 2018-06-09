using System;
using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Cache.Interfaces;

namespace BeatTheStreak.Repositories
{
	public class CachedPitcherRepository : IPitcherRepository
	{
		private readonly IPitcherRepository _decoratedComponent;
		private readonly ICacheRepository _cache;

		public CachedPitcherRepository(
			IPitcherRepository decoratedComponent,
			ICacheRepository cache)
		{
			_decoratedComponent = decoratedComponent;
			_cache = cache;
		}

		public ProbablePitcherViewModel Submit(DateTime gameDate, bool homeOnly = false)
		{
			var homePara = homeOnly ? "home" : "all";
			var keyValue = $"pitchers:{homePara}:{Utility.UniversalDate(gameDate)}";
			if (!_cache.TryGet(keyValue, out ProbablePitcherViewModel viewModel))
			{
				viewModel = _decoratedComponent.Submit(gameDate, homeOnly);
				_cache.Set(keyValue, viewModel);
			}
			return viewModel;
		}
	}
}
