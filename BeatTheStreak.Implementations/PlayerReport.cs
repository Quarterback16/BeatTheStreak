using BeatTheStreak.Models;
using FbbEventStore;
using System;
using System.IO;

namespace BeatTheStreak.Implementations
{
	public class PlayerReport
	{
		public string Player { get; set; }

		public string FantasyTeam { get; set; }

		public int JerseyNumber { get; set; }
		public bool DoPitchers { get; set; }

		public StreamWriter Writer { get; set; }
		public FileStream OutStream { get; set; }
		public string OutputFile { get; set; }

		protected void DisplayHeading(
			PlayerGameLogViewModel log,
			IRosterMaster rosterMaster)
		{
			Console.WriteLine(log.HeaderLine());
			Console.WriteLine($@"({
				JerseyNumber
				}) {
				Player
				} {
				FantasyTeam
				} {
				rosterMaster.GetMlbTeam(Player)
				} {
				rosterMaster.GetPosition(Player)
				}");
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
			FantasyTeam = fantasyTeam;
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

		protected void SetOutput()
		{
			if (string.IsNullOrEmpty(OutputFile))
				return;
			try
			{
				if (File.Exists(OutputFile))
					File.Delete(OutputFile);

				OutStream = new FileStream(
					path: OutputFile,
					mode: FileMode.OpenOrCreate,
					access: FileAccess.Write);

				Writer = new StreamWriter(OutStream);
				Console.SetOut(Writer);
				Console.WriteLine("<pre>");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Cannot open {OutputFile} for writing");
				Console.WriteLine(ex.Message);
				return;
			}
		}

		protected void CloseOutput()
		{
			if (string.IsNullOrEmpty(OutputFile))
				return;
			Console.WriteLine("</pre>");
			Writer.Close();
			OutStream.Close();
		}
	}
}
