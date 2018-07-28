using Domain;
using System;

namespace BeatTheStreak.Models
{
    public class Selection
    {
		public string Result { get; set; }
        public Batter Batter { get; set; }
        public Batter Batter1 { get; set; }
        public Batter Batter2 { get; set; }
        public Batter Batter3 { get; set; }
		public Batter Batter4 { get; set; }
		public Pitcher Pitcher { get; set; }
        public Game Game { get; set; }
        public DateTime GameDate { get; set; }

		public Selection()
		{
			Result = "     ";
		}
        public override string ToString()
        {
            return $"{Batter} vs. {Pitcher} : {Game}";
        }

        internal void DumpTop()
        {
            Console.WriteLine( Batter1 );
            Console.WriteLine( Batter2 );
            Console.WriteLine( Batter3 );
			Console.WriteLine( Batter4);
		}
    }
}
