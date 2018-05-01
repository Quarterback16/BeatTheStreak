using Domain;
using System;
using System.Collections.Generic;

namespace Application.Repositories
{
    public interface IPitcherRepository
    {
        List<Pitcher> Submit(DateTime gameDate);
    }
}
