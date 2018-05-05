using Domain;
using System;

namespace Application.Outputs
{
    public class PlayerStatsViewModel
    {
        public DateTime AsOf { get; set; }

        public Player Player { get; set; }

        public decimal AtBats { get; set; }

        public decimal PlateAppearances { get; set; }

        public decimal Hits { get; set; }

        public void Dump()
        {
            Console.WriteLine(this);
        }

        public override string ToString()
        {
            string bavg;
            if (AtBats > 0)
                bavg = string.Format("{0:#.000}", Hits / AtBats);
            else
                bavg = " .000";
            var ab = (int) AtBats;
            return $"{Player?.Name} {Hits,-2} for {ab,-3} {bavg,-5}";
        }
    }
}
