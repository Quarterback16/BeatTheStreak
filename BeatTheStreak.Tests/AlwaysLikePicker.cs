using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;

namespace BeatTheStreak.Tests
{
    public class AlwaysLikePicker 
    {
        private readonly ILineupRepository _lineupRepository;

        public AlwaysLikePicker(ILineupRepository lineupRepository)
        {
            _lineupRepository = lineupRepository;
        }

        public bool Likes(Selection selection, out string reasonForDislike)
        {
            reasonForDislike = string.Empty;
            return true;
        }

    }
}
