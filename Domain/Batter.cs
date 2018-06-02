namespace Domain
{
    public class Batter
    {
        public string PlayerSlug { get; set; }

        public string BattingOrder { get; set; }

        public string Name { get; set; }

        public string LineupPosition { get; set; }

        public string PositionAbbreviation { get; set; }

        public string Sequence { get; set; }

        public string PlayerId { get; set; }

        public string TeamId { get; set; }

        public string TeamSlug { get; set; }

        public string TeamName { get; set; }

        public bool IsSub { get; set; }

        public bool IsBatter()
        {
            return PositionAbbreviation != "P";
        }

        public decimal BattingAverage { get; set; }

        public override string ToString()
        {
            return $"{BattingOrder} {Name,-25} {BattingAverage,0:.000} {PositionAbbreviation,-2} {TeamName,-25}";
        }
    }
}
