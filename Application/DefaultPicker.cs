using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application
{
    public class DefaultPicker : BasePicker, IPicker
    {
		private List<ILike> Tests;
		private readonly ILineupProjector _lineupProjector;
		private readonly ICalculateOpponentOba _calculateOpponentOba;
		private readonly ITeamStatsRepository _teamStatsRepository;
		private ILog _logger;

        public DefaultPicker(
            IPickerOptions options,
            ILineupRepository lineupRepository,
            IPitcherRepository pitcherRepository,
			IPlayerStatsRepository playerStatsRepository,
			ITeamStatsRepository teamStatsRepository,
			ILineupProjector lineupProjector,
			ICalculateOpponentOba calculateOpponentOba,
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
			_calculateOpponentOba = calculateOpponentOba;
			_teamStatsRepository = teamStatsRepository;
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
            ProbablePitcherViewModel pitchers = GetProbablePitchers(
				gameDate);
            var i = 0;
            foreach (var pitcher in pitchers.ProbablePitchers)
            {
				if (pitcher.OpponentsBattingAverage < PickerOptions.DecimalOption(
					Constants.Options.PitchersMendozaLine))
				{
#if DEBUG
					Log($"Pitcher {pitcher} has too good a Opp B Avg");
#endif
					continue;
				}
				if (PickerOptions.OptionOn(Constants.Options.TeamClip))
				{
					var pitchersTeamClip = TeamClip(
						statsDate: gameDate.AddDays(-1),
						teamName: pitcher.TeamId);
					if (pitchersTeamClip > PickerOptions.DecimalOption(
							Constants.Options.PitchersTeamMendozaLine))
					{
#if DEBUG
						Log($"Pitchers Team {pitcher.TeamName} has too good a Clip {pitchersTeamClip}");
#endif
						continue;
					}
					var battersTeamClip = TeamClip(
						statsDate: gameDate.AddDays(-1),
						teamName: pitcher.OpponentSlug);
					if (battersTeamClip < PickerOptions.DecimalOption(
							Constants.Options.BattersTeamMendozaLine))
					{
#if DEBUG
						Log($@"Batters Team {
							pitcher.OpponentSlug
							} has not got a good enough Clip {
							battersTeamClip
							}");
#endif
						continue;
					}
				}
				++i;
                var printLine = $"{i.ToString(),2} {pitcher}";
#if DEBUG
				Log($"Looking for a {pitcher.OpponentSlug} batter facing {pitcher}");
#endif
				var lineupQueryDate = gameDate.AddDays(-1);
				var lineupPositionsToExamine = PickerOptions.IntegerOption(
					Constants.Options.LineupPositions);

				var opponents = _lineupProjector.ProjectLineup(
					pitcher, 
					lineupQueryDate,
					lineupPositionsToExamine);

				if (opponents.Lineup.Count.Equals(0))
				{
#if DEBUG
					Log("  cold team - skip this pitcher");
#endif
					continue;  //  cold team
				}
#if DEBUG
				opponents.DumpLineup();
#endif
                var batter1 = opponents.BattingAt("1");
				var batter2 = new Batter();
				var batter3 = new Batter();
				var batter4 = new Batter();

				if (lineupPositionsToExamine > 1)
				{
					batter2 = opponents.BattingAt("2");
					if (lineupPositionsToExamine > 2 )
						batter3 = opponents.BattingAt("3");
					if (lineupPositionsToExamine > 3)
						batter4 = opponents.BattingAt("4");
				}
                if (batter1 != null)
                {
                    var selection = new Selection
                    {
                        GameDate = gameDate,
                        Batter1 = batter1,
                        Batter2 = batter2,
                        Batter3 = batter3,
						Batter4 = batter4,
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
#if DEBUG
							Log($"{selection} no good :- {reason}");
#endif
							break;  // one strike and ur out
						}
					}
                    if (like)
                    {
                        batters.Add(selection);
#if DEBUG
						Log($"{selection} good");
#endif
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

		private decimal TeamClip(DateTime statsDate, string teamName)
		{
			var results = _teamStatsRepository.Submit(statsDate, teamName);
			return results.Clip();
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
#if DEBUG
			var lines = pitchers.Dump();
			LogLines(lines);
#endif
			if (pitchers.ProbablePitchers.Count == 0)
				_logger.Info($"No games scheduled for {gameDate.ToShortDateString()}");
			foreach (var pitcher in pitchers.ProbablePitchers)
			{
				if (pitcher.TeamId == null) pitcher.TeamId = pitcher.TeamSlug;
				if (pitcher.TeamSlug == null) pitcher.TeamSlug = pitcher.TeamId;

				var oba = _calculateOpponentOba.CalculateOba(
						pitcher.Slug,
						gameDate,
						PickerOptions.IntegerOption(
							Constants.Options.PitcherDaysBack));
				if (oba > 0.0M)
				{
#if DEBUG
					_logger.Trace($@"pitcher {
						pitcher.Slug
						} oba changed to {
						oba
						} from {
						pitcher.OpponentsBattingAverage
						}");
#endif
					pitcher.OpponentsBattingAverage = oba;
				}
			}
			pitchers.ProbablePitchers 
				= pitchers.ProbablePitchers.OrderByDescending(
						o => o.OpponentsBattingAverage).ToList();
#if DEBUG
			lines = pitchers.Dump();
			LogLines(lines);
#endif
			return pitchers;
        }

		private void LogLines(List<string> lines)
		{
			if (_logger == null) return;
			foreach (var line in lines)
			{
				_logger.Trace(line);
			}
		}

		public IPickerOptions GetOptions()
		{
			return PickerOptions;
		}
	}
}
