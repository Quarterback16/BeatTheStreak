namespace Domain
{
    public class Batter
    {
        public string BattingOrder { get; set; }

        public string Name { get; set; }

        public string LineupPosition { get; set; }

        public string PositionAbbreviation { get; set; }

        public string Sequence { get; set; }

        public string PlayerId { get; set; }

        public string TeamId { get; set; }

        public string TeamSlug { get; set; }

        public string TeamName { get; set; }

        public override string ToString()
        {
            return $"{BattingOrder} {Name,-25} {PositionAbbreviation,-2} {TeamName,-25}";
        }
    }
}
