using Application.Outputs;
using Application.Repositories;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class PickBatters
    {
        private readonly IPickBatters _picker;
        private readonly ILineupRepository _lineupRepository;
        private readonly IPitcherRepository _pitcherRepository;

        public PickBatters(
            IPickBatters batterPicker,
            ILineupRepository lineupRepository,
            IPitcherRepository pitcherRepository)
        {
            _picker = batterPicker;
            _lineupRepository = lineupRepository;
            _pitcherRepository = pitcherRepository;
        }

        public BatterReport Choose( DateTime gameDate, int numberRequired )
        {
            var report = new BatterReport(gameDate)
            {
                Selections = SelectBatters(gameDate, numberRequired)
            };
            return report;
        }

        private List<Selection> SelectBatters(DateTime gameDate, int numberRequired)
        {
            var batters = new List<Selection>();
            //  Get list of pitchers first
            var pitchers = _pitcherRepository.Submit(gameDate);
            foreach (var pitcher in pitchers)
            {
                var opponentTeam = pitcher.OpponentSlug;
                var result = _lineupRepository.Submit(
                    queryDate: gameDate.AddDays(-1),
                    teamSlug: opponentTeam);
                var batter = result.FirstOrDefault();
                if ( batter != null)
                {
                    var selection = new Selection
                    {
                        Batter = batter,
                        Pitcher = pitcher,
                        Game = new Game
                        {
                            Title = pitcher.NextOpponent
                        }
                    };
                    //  could have multiple pickers
                    if (_picker.Likes(selection))
                        batters.Add(selection);
                }
                if (batters.Count == numberRequired)
                    break;
            }
            return batters;
        }
    }
}
