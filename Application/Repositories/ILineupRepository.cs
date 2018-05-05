using Application.Outputs;
using System;


namespace Application.Repositories
{
    public interface ILineupRepository
    {
        LineupViewModel Submit(DateTime queryDate, string teamSlug);
    }
}
