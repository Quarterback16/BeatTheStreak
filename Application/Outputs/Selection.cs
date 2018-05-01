using Domain;
using System;

namespace Application.Outputs
{
    public class Selection
    {
        public Batter Batter { get; set; }
        public Pitcher Pitcher { get; set; }
        public Game Game { get; set; }
        public DateTime GameDate { get; set; }

        public override string ToString()
        {
            return $"{Batter} vs. {Pitcher} : {Game}";
        }
    }
}
