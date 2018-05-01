using System;
using System.Collections.Generic;
using Application.StattlleShipApi;
using Domain;

namespace Application.Repositories
{
    public class LineupRepository : ILineupRepository
    {
        public List<Batter> Submit(DateTime queryDate, string teamSlug)
        {
            var lineup = new List<Batter>();

            var lineupRequest = new LineupRequest();

            lineup = lineupRequest.Submit(
                queryDate: queryDate,
                teamSlug: teamSlug);

            return lineup;
        }
    }
}
