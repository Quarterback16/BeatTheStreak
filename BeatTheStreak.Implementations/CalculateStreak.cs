using System;
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
				var selection = _picker.Choose(gameDate, 2);
				//selection.Dump();
				if ( selection.Selections.Count > 0)
				{
					foreach (var batter in selection.Selections)
					{
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
	}
}
