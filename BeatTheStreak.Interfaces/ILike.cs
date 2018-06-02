using BeatTheStreak.Models;

namespace BeatTheStreak.Interfaces
{
	public interface ILike
	{
		bool Likes(Selection selection, out string reasonForDislike);
	}
}
