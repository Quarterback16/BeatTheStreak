using System;
using System.Collections.Generic;

namespace Application.Outputs
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
                foreach (var selection in Selections)
                {
                    Console.WriteLine(selection);
                }
            }
        }
    }
}
