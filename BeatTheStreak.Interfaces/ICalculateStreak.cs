using BeatTheStreak.Models;
using System;

namespace BeatTheStreak.Interfaces
{
	public interface ICalculateStreak
	{
		StreakViewModel StreakFor(
			DateTime startDate, 
			DateTime endDate);
	}
}
