using BeatTheStreak.Interfaces;
using System;
using System.Collections.Generic;

namespace Application
{
    public class BasePicker
    {
        protected readonly ILineupRepository _lineupRepository;
        protected readonly IPitcherRepository _pitcherRepository;

        public string PickerName { get; set; }

        public Dictionary<string,string> PickerOptions { get; set; }

        public BasePicker(
            ILineupRepository lineupRepository,
            IPitcherRepository pitcherRepository)
        {
            _lineupRepository = lineupRepository;
            _pitcherRepository = pitcherRepository;
            PickerOptions = new Dictionary<string, string>();
        }

        protected bool OptionOn(string option)
        {
            if (   PickerOptions.ContainsKey(option)
                && PickerOptions[option].ToUpper().Equals("ON"))
                return true;

            return false;
        }

        protected int IntegerOption(string option)
        {
            int val = 0;
            if (PickerOptions.ContainsKey(option))
            {
                val = Int32.Parse(PickerOptions[option]);
            }
            return val;
        }
    }
}
