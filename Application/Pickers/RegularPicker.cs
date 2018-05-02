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

        public bool Likes(Selection selection)
        {
            //var lineup1 = _lineupRepository.Submit(
            //    selection.GameDate,
            //    selection.Batter.TeamSlug);

            //if (!LineupHas(selection.Batter, lineup1))
            //    return false;

            return true;
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
