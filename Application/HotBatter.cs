using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;

namespace Application
{
	public class HotBatter : ILike
	{
		private readonly IPickerOptions _pickerOptions;

		public HotBatter(
			IPickerOptions pickerOptions)
		{
			_pickerOptions = pickerOptions;
		}

		public bool Likes(Selection selection, out string reasonForDislike)
		{
			reasonForDislike = string.Empty;

			if (!_pickerOptions.OptionOn(Constants.Options.HotBatters))
				return true;

			if (selection.Batter.BattingAverage > _pickerOptions.DecimalOption(
				Constants.Options.HotBattersMendozaLine))
				return true;
			reasonForDislike = $" {selection.Batter.PlayerSlug} Batting Avg too low";
			return false;
		}

	}
}
