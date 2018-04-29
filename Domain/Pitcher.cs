namespace Domain
{
    public class Pitcher
    {
        public string Name { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public decimal Era { get; set; }

        public string PlayerId { get; set; }

        public string TeamId { get; set; }

        public string NextOpponent { get; set; }

        public string OpponentId { get; set; }

        public override string ToString()
        {
            return $"{Name} {TeamId} ({Wins}-{Losses}) {Era:0.00} {NextOpponent} {OpponentId}";
        }
    }
}
