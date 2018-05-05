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
            ProbablePitcherViewModel pitchers = GetProbablePitchers(gameDate);
            var i = 0;
            foreach (var pitcher in pitchers.ProbablePitchers)
            {
                ++i;
                var printLine = $"{i.ToString(),2} {pitcher}";
                var opponentTeam = pitcher.OpponentSlug;
                var lineupQueryDate = gameDate.AddDays(-1);
                var result = _lineupRepository.Submit(
                    queryDate: lineupQueryDate,
                    teamSlug: opponentTeam);
                var batter = result.Lineup.FirstOrDefault();
                if (batter != null)
                {
                    var selection = new Selection
                    {
                        GameDate = gameDate,
                        Batter = batter,
                        Pitcher = pitcher,
                        Game = new Game
                        {
                            Title = pitcher.NextOpponent
                        }
                    };
                    //  could have multiple pickers
                    string reason = string.Empty;
                    if (!_picker.Likes(selection, out reason))
                        printLine += reason;
                    else
                    {
                        batters.Add(selection);
                        printLine += "  " + selection.Batter.ToString();
                    }
                }
                else
                {
                    printLine +=  
                        $@"  skipped No {
                            opponentTeam
                            } lineups available for {
                            lineupQueryDate.ToShortDateString()
                            } team COLD";
                }
                Console.WriteLine(printLine);
                if (batters.Count == numberRequired)
                    break;
            }
            return batters;
        }

        private ProbablePitcherViewModel GetProbablePitchers(DateTime gameDate)
        {
            Console.WriteLine($"GameDate {gameDate.ToLongDateString()} (US)");
            var pitchers = _pitcherRepository.Submit(gameDate);
            pitchers.Dump();
            return pitchers;
        }
    }
}
