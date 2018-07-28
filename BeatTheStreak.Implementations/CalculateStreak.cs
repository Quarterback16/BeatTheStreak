using System;
using Application;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;

namespace BeatTheStreak.Implementations
{
	public class CalculateStreak : ICalculateStreak
	{
		private readonly IPicker _picker;
		private readonly IResultChecker _resultChecker;

		public CalculateStreak(
			IPicker picker,
			IResultChecker resultChecker)
		{
			_picker = picker;
			_resultChecker = resultChecker;
		}

		public StreakViewModel StreakFor(
			DateTime startDate,
			DateTime endDate)
		{
			var result = new StreakViewModel
			{
				StartDate = startDate,
				EndDate = endDate,
				OptionSettings = _picker.GetOptions().OptionStrings()
			};
			TimeSpan range = endDate - startDate;
			for (int i = 0; i < range.TotalDays+1; i++)
			{
				var gameDate = startDate.AddDays(i);
				var selection = _picker.Choose(
					gameDate: gameDate,
					numberRequired: NumberOfBattersRequired());
				//selection.Dump();
				if ( selection.Selections.Count > 0)
				{
					foreach (var batter in selection.Selections)
					{
						if (batter == null) continue;
						batter.Result = _resultChecker.Result(
							batter.Batter,
							gameDate);
					}
				}
				var gameDay = new GameDayModel
				{
					GameDate = gameDate,
					Selections = selection.Selections
				};
				result.GameDays.Add(gameDay);
			}
			return result;
		}

		private int NumberOfBattersRequired()
		{
			var options = _picker.GetOptions();
			var numberOfBattersRequired = options.IntegerOption(
				optionName: Constants.Options.BattersToPick);
			if (numberOfBattersRequired == 0)
			{
				numberOfBattersRequired = 2;
			};
			return numberOfBattersRequired;
		}
	}
}
