using BeatTheStreak.Models;
using FbbEventStore;
using System;

namespace BeatTheStreak.Implementations
{
	public class PlayerReport
	{
		public string Player { get; set; }
		public int JerseyNumber { get; set; }
		public bool DoPitchers { get; set; }

		protected void DisplayHeading(PlayerGameLogViewModel log)
		{
			Console.WriteLine(log.HeaderLine());
			Console.WriteLine($"({JerseyNumber}) {Player}");
			Console.WriteLine(log.HeaderLine());
			Console.WriteLine(log.DateHeaderLine());
		}

		protected static void DisplayTotals(PlayerGameLogViewModel totalLog)
		{
			Console.WriteLine(totalLog.HeaderLine());
			Console.WriteLine(totalLog.DateLine("Total"));
			Console.WriteLine(totalLog.HeaderLine());
		}

		protected System.Collections.Generic.List<string> GetRoster(
			IRosterMaster rosterMaster,
			string fantasyTeam,
			DateTime asOf)
		{
			if (DoPitchers)
			{
				return rosterMaster.GetPitchers(
					fantasyTeam,
					asOf);
			}
			return rosterMaster.GetBatters(
				fantasyTeam,
				asOf);
		}
	}
}
