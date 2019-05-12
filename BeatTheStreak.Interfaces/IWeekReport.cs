using BeatTheStreak.Models;

namespace BeatTheStreak.Interfaces
{
	public interface IWeekReport
	{
		PlayerGameLogViewModel DumpWeek(int i);
	}
}
