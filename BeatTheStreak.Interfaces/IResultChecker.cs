using System;
using Domain;

namespace BeatTheStreak.Interfaces
{
	public interface IResultChecker
	{
		bool GotHit(Batter batter, DateTime gameDate);
		bool HadAtBat(Batter batter, DateTime gameDate);
	}
}