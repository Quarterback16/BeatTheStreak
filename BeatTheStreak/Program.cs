using Application;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Repositories;
using System;
using System.Collections.Generic;

namespace BeatTheStreak
{
    class Program
    {
        static void Main(string[] args)
        {
            var pitcherRepo = new PitcherRepository();
            var lineupRepo = new LineupRepository();
			var statsRepo = new PlayerStatsRepository();
            var pickers = new Dictionary<string, IPicker>();
            var options = new Dictionary<string, string>
            {
                { Constants.Options.HomePitchersOnly, "on" },
                { "dayOff", "on" }
            };
			var pickerOptions = new PickerOptions(options);
            var dp = new DefaultPicker(
				pickerOptions, 
				lineupRepo,
				pitcherRepo,
				statsRepo);

            pickers.Add(dp.PickerName, dp);

            foreach (var picker in pickers)
            {
                var response = picker.Value.Choose(
                    gameDate: DateTime.Now.AddDays(0), 
                    numberRequired: 2);
                response.Dump();
            }
        }

    }
}
