using System;
using Application.Outputs;
using Application.StattlleShipApi;

namespace Application.Repositories
{
    public class LineupRepository : ILineupRepository
    {
        public LineupViewModel Submit(DateTime queryDate, string teamSlug)
        {
            var result = new LineupViewModel
            {
                GameDate = queryDate
            };

            var lineupRequest = new LineupRequest();
            //Console.WriteLine($"requesting {teamSlug} lineup for {queryDate}");
            result = lineupRequest.Submit(
                queryDate: queryDate,
                teamSlug: teamSlug);

            return result;
        }
    }
}
