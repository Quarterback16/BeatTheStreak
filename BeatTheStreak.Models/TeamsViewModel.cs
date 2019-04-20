using Domain;
using System.Collections.Generic;

namespace BeatTheStreak.Models
{
	public class TeamsViewModel
	{
		public List<Team> Teams { get; set; }

		public void Dump()
		{
			foreach (var team in Teams)
			{
				System.Console.WriteLine(team.Name);
			}
		}
	}
}
