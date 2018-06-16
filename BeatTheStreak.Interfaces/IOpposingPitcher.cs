using Domain;
using System;

namespace BeatTheStreak.Interfaces
{
	public interface IOpposingPitcher
	{
		Pitcher PitcherFacing(string teamSlug, DateTime gameDate);
	}
}
