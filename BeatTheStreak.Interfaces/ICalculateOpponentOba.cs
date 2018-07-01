using System;

namespace BeatTheStreak.Interfaces
{
	public interface ICalculateOpponentOba
	{
		decimal CalculateOba(
			string playerSlug,
			DateTime gameDate,
			int daysBack);
	}
}
