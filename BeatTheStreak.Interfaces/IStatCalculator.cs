using System;

namespace BeatTheStreak.Interfaces
{
	public interface IStatCalculator
	{
		decimal Woba(
			string playerName,
			DateTime startDate,
			DateTime endDate);
	}
}
