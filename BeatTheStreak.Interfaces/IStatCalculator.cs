using Domain;
using System;

namespace BeatTheStreak.Interfaces
{
	public interface IStatCalculator
	{
		decimal Woba(
			string playerName,
			DateTime startDate,
			DateTime endDate);

		decimal Woba(
			Player player,
			DateTime startDate,
			DateTime endDate);

		decimal WobaBySlug(
			string playerSlug,
			DateTime startDate,
			DateTime endDate);

		decimal AbBySlug(
			string playerSlug,
			DateTime startDate,
			DateTime endDate);
	}
}
