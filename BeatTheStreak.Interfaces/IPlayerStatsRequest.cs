using BeatTheStreak.Models;
using System;

namespace BeatTheStreak.Interfaces
{
	public interface IPlayerStatsRequest
	{
		PlayerStatsViewModel Submit(DateTime queryDate, string playerSlug);
	}
}
