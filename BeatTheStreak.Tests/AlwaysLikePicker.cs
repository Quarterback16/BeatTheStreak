using Application;
using Application.Outputs;
using Application.Repositories;

namespace BeatTheStreak.Tests
{
    public class AlwaysLikePicker : IPickBatters
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
