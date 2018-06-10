using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Domain;
using System;
using System.Collections.Generic;

namespace Application
{
	public class MissingInAction : ILike
	{
		private readonly ILineupRepository _lineupRepository;
		private readonly IPlayerStatsRepository _playerStatsRepository;
		private readonly IPickerOptions _pickerOptions;

		public MissingInAction(
			ILineupRepository lineupRepository,
			IPlayerStatsRepository playerStatsRepository,
			IPickerOptions pickerOptions )
		{
			_lineupRepository = lineupRepository;
			_playerStatsRepository = playerStatsRepository;
			_pickerOptions = pickerOptions;
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
				// recalculate Batting Average
				RecalculateBattingAverage(batter,selection.GameDate);
				if (batter.BattingAverage > bestAvg)
				{
					batterWithBestAvg = batter;
					bestAvg = batter.BattingAverage;
				}
			}
			selection.Batter = batterWithBestAvg;
			return true;
		}

		private void RecalculateBattingAverage(Batter batter, DateTime gameDate)
		{
			if (!_pickerOptions.OptionOn(Constants.Options.HotBatters))
				return;
			var oldAvg = batter.BattingAverage;
			// calculate BAV for the period
			var queryDate = gameDate.AddDays(-1);
			var statsTo = _playerStatsRepository.Submit(
				queryDate,
				batter.PlayerSlug);
			var statsFrom = _playerStatsRepository.Submit(
				queryDate.AddDays(-_pickerOptions.IntegerOption(
					Constants.Options.HotBattersDaysBack)),
				batter.PlayerSlug);
			batter.BattingAverage = BattingAverage(statsFrom, statsTo);
			Console.WriteLine( $@"   bavg for {
				batter.PlayerSlug,-25
				} was {oldAvg:#.000} now calculated as {batter.BattingAverage:#.000}");
		}

		private decimal BattingAverage(
			PlayerStatsViewModel statsFrom,
			PlayerStatsViewModel statsTo)
		{
			var atBats = statsTo.AtBats - statsFrom.AtBats;
			if (atBats == 0) return 0.0M;
			var hits = statsTo.Hits - statsFrom.Hits;
			return hits / atBats;
		}

		private bool MissingFromLineup(
			Batter batter,
			DateTime gameDate)
		{
			if (_pickerOptions.OptionOn(Constants.Options.NoDaysOff))
			{
				for (int daysback = 1;
					daysback < _pickerOptions.IntegerOption(
							Constants.Options.DaysOffDaysBack) + 1;
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
