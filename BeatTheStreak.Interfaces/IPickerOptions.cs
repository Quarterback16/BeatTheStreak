﻿namespace BeatTheStreak.Interfaces
{
	public interface IPickerOptions
	{
		bool OptionOn(string optionName);
		int IntegerOption(string optionName);
		decimal DecimalOption(string optionName);
	}
}
