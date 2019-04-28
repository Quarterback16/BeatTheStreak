using Domain;
using System;

namespace BeatTheStreak.Interfaces
{
	public interface IStartingPitcher
	{
		Pitcher Starter(string teamSlug, DateTime gameDate);
	}
}
