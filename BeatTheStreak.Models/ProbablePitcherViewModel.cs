using Domain;
using System;
using System.Linq;
using System.Collections.Generic;

namespace BeatTheStreak.Models
{
    public class ProbablePitcherViewModel
    {
        public List<Pitcher> ProbablePitchers { get; set; }
        public DateTime GameDate { get; set; }
        public bool HomeOnly { get; set; }

        public List<string> Dump()
        {
			var lines = new List<string>();
            var homeOnlyOut = HomeOnly ? "HOME" : string.Empty;
            AddLine(lines,"-----------------------------------------------------");
			AddLine(lines, $"PROBABLE {homeOnlyOut} PITCHERS  {GameDate.ToLongDateString()} US");
			AddLine(lines, "-----------------------------------------------------");
            var i = 0;
			List<Pitcher> pitchers = ProbablePitchers
				.OrderBy(x => x.TeamName)
				.ToList();
            foreach (var pitcher in pitchers)
            {
                ++i;
				AddLine(lines, $@"{i.ToString(),2} {
                    pitcher
                    }  {
                    pitcher.OpponentSlug,-7
                    }  {
                    pitcher.NextOpponent
                    }");
            }
			AddLine(lines, "-----------------------------------------------------");
			return lines;
        }

		private void AddLine(List<string> lines, string line)
		{
			Console.WriteLine(line);
			lines.Add(line);
		}
	}
}
