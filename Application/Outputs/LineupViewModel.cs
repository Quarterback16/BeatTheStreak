using Domain;
using System;
using System.Collections.Generic;

namespace Application.Outputs
{
    public class LineupViewModel
    {
        public List<Batter> Lineup { get; set; }
        public DateTime GameDate { get; set; }
        public string TeamName { get; set; }

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
