using BeatTheStreak.Repositories;
using BeatTheStreak.Repositories.StattlleShipApi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class RosterTests
	{
		public StreamWriter Writer { get; set; }
		public FileStream OutStream { get; set; }
		public string OutputFile { get; set; }

		[TestMethod]
		public void TeamRoster_ForSeason2019_ReturnsDto()
		{
			var team = "mlb-chw";
			var sut = new RosterRequest();
			var result = sut.LoadData(
				Constants.MlbSeasons.Season2019,
				team);
			Assert.IsNotNull(result);
			Assert.IsTrue(result.Count > 0);
			DumpPlayerDto(result,team);
		}

		private void DumpPlayerDto(
			System.Collections.Generic.List<PlayerDto> result,
			string team)
		{
			OutputFile = TestHelper.FileName(
					"Rosters",
					team);
			SetOutput();
			var p = 0;
			foreach (var player in result)
			{
				//if (!player.Active)
				//	continue;
				p++;
				System.Console.WriteLine(
					$"{p:00} {player.RosterLine()}");
			}
			CloseOutput();
		}

		private void SetOutput()
		{
			if (string.IsNullOrEmpty(OutputFile))
				return;
			try
			{
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

		private void CloseOutput()
		{
			if (string.IsNullOrEmpty(OutputFile))
				return;
			Console.WriteLine("</pre>");
			Writer.Close();
			OutStream.Close();
		}
	}
}
