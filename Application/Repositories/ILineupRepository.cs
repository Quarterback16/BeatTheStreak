using Domain;
using System;
using System.Collections.Generic;


namespace Application.Repositories
{
    public interface ILineupRepository
    {
        List<Batter> Submit(DateTime queryDate, string teamSlug);
    }
}
