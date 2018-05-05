using Application.Outputs;
using System;

namespace Application.Repositories
{
    public interface IPitcherRepository
    {
        ProbablePitcherViewModel Submit(DateTime gameDate);
    }
}
