using BeatTheStreak.Helpers;
using System;
using System.Collections.Generic;

namespace BeatTheStreak.Models
{
	public class StreakViewModel
	{
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public List<string> OptionSettings { get; set; }

		public List<GameDayModel> GameDays { get; set; }
		public int BestStreak { get; set; }

		public StreakViewModel()
		{
			GameDays = new List<GameDayModel>();
		}

		public void Dump()
		{
			DumpHeading();

			foreach (var gameDay in GameDays)
			{
				Console.Write(
					Utility.UniversalDate(gameDay.GameDate));
				var batterCount = 0;
				foreach (var selection in gameDay.Selections)
				{
					batterCount++;
					if (batterCount == 2) Console.Write("          ");
					Console.WriteLine(
						$"  {selection.Result} {selection.Batter}");
				}
				Console.WriteLine();
			}
			CalculateScore();
			Console.WriteLine();
			Console.WriteLine($"Score: {BestStreak}" );
		}

		private void DumpHeading()
		{
			Console.WriteLine( $@"Beat the Streak for the period {
				Utility.UniversalDate(StartDate)
				} to {
				Utility.UniversalDate(EndDate)
				}");
			Console.WriteLine();
			Console.WriteLine("options:-"); ;
			foreach (var setting in OptionSettings)
			{
				Console.WriteLine($"    {setting}");
			}
			Console.WriteLine();
		}

		private void CalculateScore()
		{
			var currentStreak = 0;
			foreach (var gameDay in GameDays)
			{
				var dayTotal = 0;
				bool isOut = false;
				foreach (var selection in gameDay.Selections)
				{
					if (selection.Result == "OUT")
					{
						isOut = true;
						break;
					}
					else if (selection.Result == "HIT")
						dayTotal++;
				}
				if (isOut)
				{
					dayTotal = 0;
					if (currentStreak > BestStreak)
					{
						BestStreak = currentStreak;
					}
					currentStreak = 0;
				}
				else
					currentStreak += dayTotal;
				//Console.WriteLine($@"{
				//	Utility.UniversalDate(gameDay.GameDate)
				//	} curr:{currentStreak}");
			}
			if (currentStreak > BestStreak)
			{
				BestStreak = currentStreak;
			}
		}
	}
}
