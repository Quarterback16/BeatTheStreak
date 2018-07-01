using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Domain;
using System;
using System.Collections.Generic;

namespace BeatTheStreak.Implementations
{
	public class LineupProjector : ILineupProjector
	{
		private ILineupRepository _lineupRepository;
		private IOpposingPitcher _opposingPitcher;
		private ILog _logger;

		public int DaysToGoBack { get; set; }

		public LineupProjector(
			ILineupRepository lineupRepository,
			IOpposingPitcher opposingPitcher,
			ILog logger,
			int daysToGoBack)
		{
			_lineupRepository = lineupRepository;
			_opposingPitcher = opposingPitcher;
			_logger = logger;
			DaysToGoBack = daysToGoBack;
		}

		public LineupViewModel ProjectLineup(
			Pitcher pitcher, 
			DateTime lineupQueryDate)
		{
			//return BasicOpponentsLineup(pitcher,lineupQueryDate);
			return SmartLineup(pitcher, lineupQueryDate);
		}

		private LineupViewModel BasicOpponentsLineup(
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

		private LineupViewModel SmartLineup(
			Pitcher pitcher,
			DateTime lineupQueryDate)
		{
			var opponentTeam = pitcher.OpponentSlug;
			var pitcherThrows = pitcher.Throws;
			if (pitcherThrows.Equals("L"))
				DaysToGoBack = DaysToGoBack * 2;
			var result = new LineupViewModel
			{
				TeamName = opponentTeam,
				Lineup = new List<Batter>()
			};
			// workout the average lineup versus R or L
			Dictionary<string, Batter> Roster = new Dictionary<string, Batter>();
			Dictionary<int, Dictionary<string,int>> d = 
				new Dictionary<int, Dictionary<string, int>>();
			var lineupCount = 0;
			for (int i = 1; i < DaysToGoBack+1; i++)
			{
				var focusDate = lineupQueryDate.AddDays(-i);
				var lineup = _lineupRepository.Submit(
								queryDate: focusDate,
								teamSlug: opponentTeam);

				if (lineup.Lineup.Count == 0)
					continue;

				var lineupPitcher = _opposingPitcher.PitcherFacing(
					opponentTeam, focusDate);

				if (string.IsNullOrEmpty(lineupPitcher.Name))
				{
					Log($@"pitcher on {focusDate} is unknown");
					continue;
				}
				Log($@"pitcher on {
					focusDate
					} is {
					lineupPitcher.Name
					} throws {lineupPitcher.Throws}");
				if (lineupPitcher.Throws.Equals(pitcherThrows))
				{
					lineupCount++;
					for (int j = 1; j < 4; j++)
					{
						var batterAt = lineup.BattingAt(j.ToString());
						AddBatter(d, j, batterAt);
						AddToRoster(Roster, batterAt);
					}
				}
			}
			if (lineupCount > 0)
			{
				for (int j = 1; j < 4; j++)
				{
					var lastBest = 0;
					var best = string.Empty;
					var apps = d[j];
					foreach (KeyValuePair<string, int> pair in apps)
					{
						if (pair.Value > lastBest)
						{
							best = pair.Key;
							lastBest = pair.Value;
						}
					}
					var bestBatter = Roster[best];
					bestBatter.BattingOrder = j.ToString();
					bestBatter.IsSub = false;
					result.Lineup.Add(bestBatter);
				}
			}
			else
				Log($"No lineups found going back {DaysToGoBack} days");
			return result;
		}

		private void Log(string message)
		{
			Console.WriteLine(message);
			_logger.Trace(message);
		}

		private void AddToRoster(Dictionary<string, Batter> roster, Batter batterAt)
		{
			if (!roster.ContainsKey(batterAt.PlayerSlug))
				roster.Add(batterAt.PlayerSlug,batterAt);
		}

		private void AddBatter(
			Dictionary<int, Dictionary<string, int>> d, 
			int j, 
			Batter batter)
		{
			if (d.TryGetValue(j, out Dictionary<string, int> batterDict))
			{
				if (batterDict.TryGetValue(batter.PlayerSlug, out int batterAps))
				{
					batterAps++;
					batterDict[batter.PlayerSlug] = batterAps;
				}
				else
					batterDict.Add(batter.PlayerSlug, 1);
			}
			else
			{
				var playerDict = new Dictionary<string, int>
				{
					{ batter.PlayerSlug, 1 }
				};
				d.Add( j, playerDict);
			}
		}
	}
}
