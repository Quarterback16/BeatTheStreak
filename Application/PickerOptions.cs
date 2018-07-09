using BeatTheStreak.Interfaces;
using System;
using System.Collections.Generic;

namespace Application
{
	public class PickerOptions : IPickerOptions
	{
		public Dictionary<string, string> Options { get; set; }

		public PickerOptions(Dictionary<string, string> options)
		{
			Options = new Dictionary<string, string>();
			Options = options;
		}

		public decimal DecimalOption(string optionName)
		{
			decimal val = 0M;
			if (Options.ContainsKey(optionName))
			{
				val = Decimal.Parse(Options[optionName]);
			}
			return val;
		}

		public int IntegerOption(string optionName)
		{
			int val = 0;
			if (Options.ContainsKey(optionName))
			{
				val = Int32.Parse(Options[optionName]);
			}
			return val;
		}

		public bool OptionOn(string optionName)
		{
			if (Options.ContainsKey(optionName)
				&& Options[optionName].ToUpper().Equals("ON"))
				return true;

			return false;
		}

		public List<string> OptionStrings()
		{
			var options = new List<string>();
			foreach (var keyPair in Options)
			{
				options.Add($"{keyPair.Key}:{keyPair.Value}");
			}
			return options;
		}
	}
}
