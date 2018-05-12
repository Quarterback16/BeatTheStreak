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
            var choices = new List<Batter>();
            var originalChoices = new List<Batter>
            {
                selection.Batter1,
                selection.Batter2,
                selection.Batter3
            };
            foreach (var batter in originalChoices)
            {
                if ( ! MissingFromLineup(batter, selection.GameDate))
                {
                    choices.Add(batter);
                }
            }
            if (choices.Count == 0)
            {
                reasonForDislike = "  All top 3 batters had days off";
                return false;
            }
            var bestAvg = 0.000M;
            var batterWithBestAvg = new Batter();
            foreach (var batter in choices)
            {
                if (batter.BattingAverage > bestAvg)
                {
                    batterWithBestAvg = batter;
                    bestAvg = batter.BattingAverage;
                }
            }
            selection.Batter = batterWithBestAvg;
            return true;
        }

        private bool MissingFromLineup(Batter batter, DateTime gameDate)
        {
            return false;
            //for (int daysback = 1; daysback < 4; daysback++)
            //{
            //    var queryDate = gameDate.AddDays(-daysback);
            //    if (NotInLineup(queryDate, batter))
            //    {
            //        //reasonForDislike = $@"  {batter.Name} was not in the {
            //        //    batter.TeamSlug
            //        //    } line up on {
            //        //    queryDate
            //        //    }";
            //        return false;
            //    }
            //}
            //return true;
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
