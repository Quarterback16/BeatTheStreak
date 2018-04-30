namespace Domain
{
    public class Game
    {
        public string Title { get; set; }

        public override string ToString()
        {
            return $"{Title}";
        }
    }
}
