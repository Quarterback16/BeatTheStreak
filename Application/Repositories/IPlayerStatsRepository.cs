using Application.Outputs;
using System;

namespace Application.Repositories
{
    public interface IPlayerStatsRepository
    {
        PlayerStatsViewModel Submit(DateTime queryDate, string playerSlug);
    }
}
