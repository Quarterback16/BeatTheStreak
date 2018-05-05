using System;
using System.Collections.Generic;
using Application.Outputs;
using Application.Repositories;
using Domain;

namespace Application.Pickers
{
    public class RegularPicker : IPickBatters
    {
        private readonly ILineupRepository _lineupRepository;

        public RegularPicker(ILineupRepository lineupRepository)
        {
            _lineupRepository = lineupRepository;
        }

        public bool Likes(Selection selection, out string reasonForDislike)
        {
            reasonForDislike = string.Empty;
            for (int daysback = 1; daysback < 4; daysback++)
            {
                var queryDate = selection.GameDate.AddDays(-daysback);
                if (NotInLineup(queryDate, selection.Batter))
                {
                    reasonForDislike = $@"  {selection.Batter.Name} was not in the {
                        selection.Batter.TeamSlug
                        } line up on {
                        queryDate
                        }";
                        return false;
                }
            }
            return true;
        }

        private bool NotInLineup(
            DateTime gameDate, 
            Batter batter)
        {
            var lineup = _lineupRepository.Submit(
                gameDate,
                batter.TeamSlug).Lineup;

            if (!LineupHas(batter, lineup))
            {
                return true;
            }
            return false;
        }

        private bool LineupHas(Batter batter, List<Batter> lineup)
        {
            foreach (var b in lineup)
            {
                if ( b.Name == batter.Name )
                {
                    return true;
                }
            }
            return false;
        }
    }
}
