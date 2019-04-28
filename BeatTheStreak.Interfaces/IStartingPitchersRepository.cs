using BeatTheStreak.Models;
using System;

namespace BeatTheStreak.Interfaces
{
	public interface IStartingPitchersRepository
	{
		StartersViewModel Submit(DateTime queryDate);
	}
}
