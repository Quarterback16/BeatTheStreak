using Domain;

namespace BeatTheStreak.Models
{
	public class HotListViewModel
	{
		public Player Player { get; set; }

		public decimal Woba { get; set; }

		public decimal AtBats { get; set; }

		public override string ToString()
		{
			return $"{Player.Name} : {Woba:0.###} in {AtBats} AB <{Player.Slug}>";
		}
	}
}
