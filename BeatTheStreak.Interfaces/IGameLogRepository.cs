using BeatTheStreak.Models;
using System;

namespace BeatTheStreak.Interfaces
{
	public interface IGameLogRepository
	{
		PlayerGameLogViewModel Submit(DateTime queryDate, string playerSlug);
	}
}
