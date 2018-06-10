using BeatTheStreak.Models;
using System;

namespace BeatTheStreak.Interfaces
{
	public interface IGameLogRequest
	{
		PlayerGameLogViewModel Submit(DateTime queryDate, string playerSlug);
	}
}
