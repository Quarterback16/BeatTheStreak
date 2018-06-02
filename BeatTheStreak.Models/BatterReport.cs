﻿using System;
using System.Collections.Generic;

namespace BeatTheStreak.Models
{
    public class BatterReport : BaseReport
    {
        public DateTime GameDate { get; set; }

        public List<Selection> Selections { get; set; }

        public BatterReport(DateTime gameDate)
        {
            GameDate = gameDate;
        }

        public void Dump()
        {
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine($"COMPUTER PICKS   {VersionNo}");
            Console.WriteLine("-----------------------------------------------------");

            Console.WriteLine(GameDate.ToLongDateString());
            Console.WriteLine();
            if (Selections != null)
            {
                var i = 0;
                foreach (var selection in Selections)
                {
                    i++;
                    Console.WriteLine($"{i}. {selection}");
                }
				HittingResults();
			}

        }

		private void HittingResults()
		{
			if ( GameDate < DateTime.Now.AddDays(-2))
			{
				var i = 0;
				foreach (var selection in Selections)
				{
					i++;
					var slug = selection.Batter.PlayerSlug;
					Console.WriteLine($"{i}. {selection}");
				}
			}
		}
	}
}
