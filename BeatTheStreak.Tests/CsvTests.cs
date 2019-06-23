using BeatTheStreak.Helpers;
using BeatTheStreak.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace BeatTheStreak.Tests
{
	[TestClass]
	public class CsvTests : BtsBaseTests
	{
		/// <summary>
		///   CSV output will be a by-product of the WeekReport
		/// </summary>

		[TestInitialize]
		public void Setup()
		{
			Initialize();
		}

		[TestMethod]
		public void CsvFile_Exists()
		{
			var sut = new CsvFile(@"./Data/");
			var result = sut.Exists("GameLog");
			result.OnFailure(() => Console.WriteLine(
					$"Error:{result.Error}"));

			Assert.IsFalse(result.IsSuccess);
		}

		[TestMethod]
		public void CsvFileCreate_WhenThereIsNoHeader_Fails()
		{
			var sut = new CsvFile(@"./Data/");
			var result = sut.Create();
			result.OnFailure(() => Console.WriteLine(
					$"Error:{result.Error}"));
			Assert.IsFalse(result.IsSuccess);
		}

		[TestMethod]
		public void CsvFileAppend_WhenThereIsData_Appends()
		{
			var sut = new CsvFile(@"./Data/")
			{
				FilePath = "GameLog",
				StartNew = true,
			};
			var createResult = sut.Create();
			Assert.IsTrue(createResult.IsSuccess);
			var metrics = new string[11];
			metrics[0] = "2019-06-22";
			metrics[1] = "mlb-josh-bell";
			metrics[2] = "2";
			metrics[3] = "4";
			metrics[4] = "1";
			metrics[5] = "1";
			metrics[6] = "6";
			metrics[7] = "3";
			metrics[8] = "0";
			metrics[9] = "1";
			metrics[10] = "0";
			var result = sut.AppendLine(metrics);
			result.OnFailure(() => Console.WriteLine(
					$"Error:{result.Error}"));
			Assert.IsTrue(result.IsSuccess);
			Assert.IsTrue(File.Exists(@".\Data\GameLog.csv"));
		}

		[TestMethod]
		public void CsvFileCreate_WhenThereIsHeader_CreatesFile()
		{
			var sut = new CsvFile(@"./Data/")
			{
				Header = "Date,Player,Hits",
				FilePath = "GameLog"
			};
			var result = sut.Create();
			result.OnFailure(() => Console.WriteLine(
					$"Error:{result.Error}"));
			Assert.IsTrue(result.IsSuccess);
			Assert.IsTrue(File.Exists(@".\Data\GameLog.csv"));
		}

		[TestMethod]
		public void Bts_CanGenerate_CsvData()
		{
			var week = Utility.CurrentWeek();
			var sut = new TeamReport(
				new WeekReport(
					//_gameLogRepository,
					_cachedGameLogRepository,
					_rosterMaster),
				_rosterMaster)
			{
				WeekStarts = Utility.WeekStart(week),
				FantasyTeam = "CA",
				Hitters = true
			};
			sut.DumpCsv(@".\Data\", startNew: true);
			Assert.IsTrue(File.Exists(@".\Data\GameLog.csv"));
		}
	}
}
