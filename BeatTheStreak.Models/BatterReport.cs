using System;
using System.Collections.Generic;
using System.Text;

namespace BeatTheStreak.Models
{
    public class BatterReport : BaseReport
    {
        public DateTime GameDate { get; set; }

        public List<Selection> Selections { get; set; }

        public BatterReport(
			DateTime gameDate)
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
                    Console.WriteLine($"{i}. {selection.Result} {selection}");
                }
			}

        }


		public string AsString()
		{
			var sb = new StringBuilder();
			sb.AppendLine("-----------------------------------------------------");
			sb.AppendLine($"COMPUTER PICKS   {VersionNo}");
			sb.AppendLine("-----------------------------------------------------");

			sb.AppendLine(GameDate.ToLongDateString());
			sb.AppendLine();
			if (Selections != null)
			{
				var i = 0;
				foreach (var selection in Selections)
				{
					i++;
					sb.AppendLine($"{i}. {selection.Result} {selection}");
				}
			}
			return sb.ToString();
		}
	}
}
