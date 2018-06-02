using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BeatTheStreak.Models
{
    public class LineupViewModel
    {
        public List<Batter> Lineup { get; set; }
        public DateTime GameDate { get; set; }
        public string TeamName { get; set; }

        public Batter BattingAt( string battingOrder )
        {
            foreach (var batter in Lineup)
            {
                if (batter.BattingOrder.Equals(battingOrder)
                    && batter.IsBatter()
                    && ! batter.IsSub)
                    return batter;
            }
            return Lineup.FirstOrDefault();
        }

        public void DumpLineup()
        {
            Console.WriteLine($"Lineup {TeamName} {GameDate.ToShortDateString()}");
            var pad = string.Empty;
            var lastPos = "X";
            foreach (var batter in Lineup)
            {
                if (Int32.Parse(batter.BattingOrder) > 0)
                {
                    if (lastPos == batter.BattingOrder)
                        pad = "   ";
                    Console.WriteLine($"{pad} {batter}");
                    lastPos = batter.BattingOrder;
                    pad = string.Empty;
                }
            }
        }
    }
}
