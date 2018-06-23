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
		private readonly ILineupProjector _lineupProjector;
		private ILog _logger;

        public DefaultPicker(
            IPickerOptions options,
            ILineupRepository lineupRepository,
            IPitcherRepository pitcherRepository,
			IPlayerStatsRepository playerStatsRepository,
			ILineupProjector lineupProjector,
			ILog logger
			) 
            : base(lineupRepository, pitcherRepository)
        {
            PickerName = "Default Picker";
            PickerOptions = options;
			Tests = new List<ILike>
			{
				new MissingInAction(lineupRepository,playerStatsRepository,options),
				new HotBatter(options)
			};
			_lineupProjector = lineupProjector;
			_logger = logger;
		}

        public BatterReport Choose( DateTime gameDate, int numberRequired )
        {
            var report = new BatterReport(gameDate)
            {
                Selections = SelectBatters(gameDate, numberRequired)
            };
            return report;
        }

        private List<Selection> SelectBatters(
			DateTime gameDate,
			int numberRequired)
        {
            var batters = new List<Selection>();
            ProbablePitcherViewModel pitchers = GetProbablePitchers(gameDate);
            var i = 0;
            foreach (var pitcher in pitchers.ProbablePitchers)
            {
				if (pitcher.OpponentsBattingAverage < PickerOptions.DecimalOption(
					Constants.Options.PitchersMendozaLine))
				{
					Log($"Pitcher {pitcher} has too good a Opp B Avg");
					continue;
				}
                ++i;
                var printLine = $"{i.ToString(),2} {pitcher}";
				Log($"Looking for a {pitcher.OpponentSlug} batter facing {pitcher}");
                var lineupQueryDate = gameDate.AddDays(-1);

                var opponents = _lineupProjector.ProjectLineup(
					pitcher, 
					lineupQueryDate);

				if (opponents.Lineup.Count.Equals(0))
				{
					Log("  cold team - skip this pitcher");
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

                if (batters.Count == numberRequired)
                    break;
            }
            return batters;
        }

		private void Log(string message)
		{
			Console.WriteLine(message);
			_logger.Info(message);
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

        private ProbablePitcherViewModel GetProbablePitchers(
			DateTime gameDate)
        {
            var pitchers = _pitcherRepository.Submit(
                gameDate,
                homeOnly: PickerOptions.OptionOn(
					Constants.Options.HomePitchersOnly));
            var lines = pitchers.Dump();
			LogLines(lines);
            return pitchers;
        }

		private void LogLines(List<string> lines)
		{
			if (_logger == null) return;
			foreach (var line in lines)
			{
				_logger.Info(line);
			}
		}
	}
}
