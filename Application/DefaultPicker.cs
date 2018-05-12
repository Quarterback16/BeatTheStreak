using Application.Outputs;
using Application.Repositories;
using Domain;
using System;
using System.Collections.Generic;

namespace Application
{
    public class DefaultPicker : BasePicker
    {
        private readonly IPickBatters _picker;

        public DefaultPicker(
            Dictionary<string,string> options,
            IPickBatters batterPicker,
            ILineupRepository lineupRepository,
            IPitcherRepository pitcherRepository) 
            : base(lineupRepository, pitcherRepository)
        {
            PickerName = "Default Picker";
            PickerOptions = options;
            _picker = batterPicker;
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
                var lineupQueryDate = gameDate.AddDays(-1);

                var opponents = GetOpponentsLineup(pitcher, lineupQueryDate);

                if (opponents.Lineup.Count.Equals(0))
                    continue;  //  cold team

                var batter1 = opponents.BattingAt("1");
                var batter2 = opponents.BattingAt("2");
                var batter3 = opponents.BattingAt("3");
                if (batter1 != null)
                {
                    var selection = new Selection
                    {
                        GameDate = gameDate,
                        Batter1 = batter1,
                        Batter2 = batter2,
                        Batter3 = batter3,
                        Pitcher = pitcher,
                        Game = new Game
                        {
                            Title = pitcher.NextOpponent
                        }
                    };
                    //TODO:  could have multiple pickers
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
                            pitcher.OpponentSlug
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

        private LineupViewModel GetOpponentsLineup(
            Pitcher pitcher, 
            DateTime lineupQueryDate)
        {
            var opponentTeam = pitcher.OpponentSlug;
            var result = _lineupRepository.Submit(
                queryDate: lineupQueryDate,
                teamSlug: opponentTeam);
            return result;
        }

        private ProbablePitcherViewModel GetProbablePitchers(DateTime gameDate)
        {
            Console.WriteLine($"GameDate {gameDate.ToLongDateString()} (US)");
            var pitchers = _pitcherRepository.Submit(
                gameDate,
                homeOnly: OptionOn(Constants.Options.HomePitchersOnly));
            pitchers.Dump();
            return pitchers;
        }
    }
}
