using System.Collections.Generic;

namespace BeatTheStreak.Interfaces
{
	public interface IPickerOptions
	{
		bool OptionOn(string optionName);
		int IntegerOption(string optionName);
		decimal DecimalOption(string optionName);
		List<string> OptionStrings();
	}
}
