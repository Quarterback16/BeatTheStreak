using Domain;
using System;
using System.Collections.Generic;

namespace Application.Outputs
{
    public class ProbablePitcherViewModel
    {
        public List<Pitcher> ProbablePitchers { get; set; }
        public DateTime GameDate { get; set; }
        public bool HomeOnly { get; set; }

        public void Dump()
        {
            var homeOnlyOut = HomeOnly ? "HOME" : string.Empty;
            Console.WriteLine("-----------------------------------------------------");
            Console.WriteLine($"PROBABLE {homeOnlyOut} PITCHERS  {GameDate.ToLongDateString()} US");
            Console.WriteLine("-----------------------------------------------------");
            var i = 0;
            foreach (var pitcher in ProbablePitchers)
            {
                ++i;
                Console.WriteLine($@"{i.ToString(),2} {
                    pitcher
                    }  {
                    pitcher.OpponentSlug,-7
                    }  {
                    pitcher.NextOpponent
                    }");
            }
            Console.WriteLine("-----------------------------------------------------");
        }
    }
}
