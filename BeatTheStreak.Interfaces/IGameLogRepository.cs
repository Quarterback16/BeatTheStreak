using BeatTheStreak.Models;
using BeatTheStreak.Helpers;
using System;

namespace BeatTheStreak.Interfaces
{
	public interface IGameLogRepository
	{
		Result<PlayerGameLogViewModel> Submit(
			DateTime queryDate,
			string playerSlug);
	}
}
