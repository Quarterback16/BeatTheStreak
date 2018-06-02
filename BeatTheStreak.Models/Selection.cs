using Domain;
using System;

namespace BeatTheStreak.Models
{
    public class Selection
    {
        public Batter Batter { get; set; }
        public Batter Batter1 { get; set; }
        public Batter Batter2 { get; set; }
        public Batter Batter3 { get; set; }
        public Pitcher Pitcher { get; set; }
        public Game Game { get; set; }
        public DateTime GameDate { get; set; }

        public override string ToString()
        {
            return $"{Batter} vs. {Pitcher} : {Game}";
        }

        internal void DumpTop3()
        {
            Console.WriteLine( Batter1 );
            Console.WriteLine( Batter2 );
            Console.WriteLine( Batter3 );
        }
    }
}
