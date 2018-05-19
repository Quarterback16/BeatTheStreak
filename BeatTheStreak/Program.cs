using Application;
using Application.Repositories;
using System;
using System.Collections.Generic;
using System.IO;

namespace BeatTheStreak
{
    class Program
    {
        static void Main(string[] args)
        {
            var pitcherRepo = new PitcherRepository();
            var lineupRepo = new LineupRepository();
            var pickers = new Dictionary<string, IPicker>();
            var options = new Dictionary<string, string>
            {
                { Constants.Options.HomePitchersOnly, "on" },
                { "dayOff", "on" }
            };

            var dp = new DefaultPicker(options,lineupRepo,pitcherRepo);

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
