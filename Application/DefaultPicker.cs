using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Domain;
using System;
using System.Collections.Generic;

namespace Application
{
    public class DefaultPicker : BasePicker, IPicker
    {
		private List<ILike> Tests;

        public DefaultPicker(
            IPickerOptions options,
            ILineupRepository lineupRepository,
            IPitcherRepository pitcherRepository,
			IPlayerStatsRepository playerStatsRepository) 
            : base(lineupRepository, pitcherRepository)
        {
            PickerName = "Default Picker";
            PickerOptions = options;
			Tests = new List<ILike>
			{
				new MissingInAction(lineupRepository,playerStatsRepository,options),
				new HotBatter(options),
			};
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
				Console.WriteLine($"Looking for a batter facing {pitcher}");
                var lineupQueryDate = gameDate.AddDays(-1);

                var opponents = GetOpponentsLineup(pitcher, lineupQueryDate);

				if (opponents.Lineup.Count.Equals(0))
				{
					Console.WriteLine("  cold team - skip this pitcher");
					continue;  //  cold team
				}

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
					//  additive
					var like = true;
                    string reason = string.Empty;
					foreach (var test in Tests)
					{
						if (!test.Likes(selection, out reason))
						{
							printLine += reason;
							like = false;
							break;  // one strike and ur out
						}
					}
                    if (like)
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
			if (result.Lineup.Count.Equals(0))
			{
				//  go back one more day
				result = _lineupRepository.Submit(
								queryDate: lineupQueryDate.AddDays(-1),
								teamSlug: opponentTeam);
			}

			return result;
        }

        private ProbablePitcherViewModel GetProbablePitchers(DateTime gameDate)
        {
            Console.WriteLine($"GameDate {gameDate.ToLongDateString()} (US)");
            var pitchers = _pitcherRepository.Submit(
                gameDate,
                homeOnly: PickerOptions.OptionOn(Constants.Options.HomePitchersOnly));
            pitchers.Dump();
            return pitchers;
        }

        //public bool Likes(Selection selection, out string reasonForDislike)
        //{
        //    reasonForDislike = string.Empty;
        //    var choices = new List<Batter>();
        //    var originalChoices = new List<Batter>
        //    {
        //        selection.Batter1,
        //        selection.Batter2,
        //        selection.Batter3
        //    };
        //    foreach (var batter in originalChoices)
        //    {
        //        if (!MissingFromLineup(
        //            batter,
        //            selection.GameDate))
        //        {
        //            choices.Add(batter);
        //        }
        //    }
        //    if (choices.Count == 0)
        //    {
        //        reasonForDislike = $@"  All top 3 batters for {
        //            selection.Batter1.TeamSlug
        //            } had time off in the last 3 days";
        //        return false;
        //    }
        //    var bestAvg = 0.000M;
        //    var batterWithBestAvg = new Batter();
        //    foreach (var batter in choices)
        //    {
        //        if (batter.BattingAverage > bestAvg)
        //        {
        //            batterWithBestAvg = batter;
        //            bestAvg = batter.BattingAverage;
        //        }
        //    }
        //    selection.Batter = batterWithBestAvg;
        //    return true;
        //}

    }
}
