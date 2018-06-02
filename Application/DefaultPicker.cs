using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Domain;
using System;
using System.Collections.Generic;

namespace Application
{
    public class DefaultPicker : BasePicker, IPicker
    {
        public DefaultPicker(
            Dictionary<string,string> options,
            ILineupRepository lineupRepository,
            IPitcherRepository pitcherRepository) 
            : base(lineupRepository, pitcherRepository)
        {
            PickerName = "Default Picker";
            PickerOptions = options;
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
                    if (!Likes(selection, out reason))
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
                if (!MissingFromLineup(
                    batter,
                    selection.GameDate))
                {
                    choices.Add(batter);
                }
            }
            if (choices.Count == 0)
            {
                reasonForDislike = $@"  All top 3 batters for {
                    selection.Batter1.TeamSlug
                    } had time off in the last 3 days";
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

        private bool MissingFromLineup(
            Batter batter,
            DateTime gameDate)
        {
            if (OptionOn(Constants.Options.NoDaysOff))
            {
                for (int daysback = 1; 
                    daysback < IntegerOption(Constants.Options.DaysOffDaysBack) + 1;
                    daysback++)
                {
                    var queryDate = gameDate.AddDays(-daysback);
                    if (NotInLineup(queryDate, batter))
                    {
                        return true;
                    }
                }
            }
            return false;
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
                if (b.Name == batter.Name)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
