using System;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Domain;

namespace BeatTheStreak.Implementations
{
	public class OpposingPitcher : IOpposingPitcher
	{
		private readonly IPitcherRepository _pitcherRepository;
		public ProbablePitcherViewModel Pitchers { get; set; }

		public OpposingPitcher(IPitcherRepository pitcherRepository)
		{
			_pitcherRepository = pitcherRepository;
		}

		public Pitcher PitcherFacing(string teamSlug, DateTime gameDate)
		{
			var pitcherFacing = new Pitcher();
			Pitchers = GetProbablePitchers(gameDate);
			foreach (var pitcher in Pitchers.ProbablePitchers)
			{
				if (pitcher.OpponentSlug.Equals(teamSlug))
				{
					pitcherFacing = pitcher;
					break;
				}
			}
			return pitcherFacing;
		}

		private ProbablePitcherViewModel GetProbablePitchers(DateTime gameDate)
		{
			var pitchers = _pitcherRepository.Submit(
				gameDate,
				homeOnly: false);
			return pitchers;
		}
	}
}
