using BeatTheStreak.Models;
using System;

namespace BeatTheStreak.Interfaces
{
	public interface IGameLogRequest
	{
		PlayerStatsViewModel Submit(DateTime queryDate, string playerSlug);
	}
}
