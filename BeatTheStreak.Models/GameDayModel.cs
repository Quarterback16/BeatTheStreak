using System;
using System.Collections.Generic;

namespace BeatTheStreak.Models
{
	public class GameDayModel
	{
		public DateTime GameDate { get; set; }
		public List<Selection> Selections { get; set; }
	}
}
