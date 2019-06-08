using BeatTheStreak.Interfaces;
using FbbEventStore;
using System;
using System.Collections.Generic;

namespace BeatTheStreak.Implementations
{
	public class HotList : IHotList
	{
		private readonly IActualRoster _actualRoster;
		private readonly IRosterMaster _rosterMaster;
		private readonly IStatCalculator _statCalculator;

		public HotList(
			IActualRoster actualRoster,
			IRosterMaster rosterMaster,
			IStatCalculator statCalculator)
		{
			_actualRoster = actualRoster;
			_rosterMaster = rosterMaster;
			_statCalculator = statCalculator;
		}

		public List<string> GetHotList(
			List<string> teamSlugs,
			DateTime queryDate, 
			int gamesBack)
		{
			var rostered = _actualRoster.GetActualRoster(
				teamSlugs,
				queryDate,
				gamesBack,
				battersOnly: true);

			var freeAgents = FilterOutSignedPlayers(
				rostered);
			var hotList = FilterOutPlayersBelow(
				0.400M,
				freeAgents,
				StartDate(queryDate, gamesBack),
				queryDate);
			return hotList;
		}

		private DateTime StartDate(
			DateTime endDate, 
			int gamesBack)
		{
			return endDate.AddDays(0-(gamesBack+1));
		}

		private List<string> FilterOutPlayersBelow(
			decimal threshold, 
			List<string> freeAgents,
			DateTime startDate,
			DateTime endDate)
		{
			var hotList = new List<string>();
			foreach (var player in freeAgents)
			{
				var woba = _statCalculator.Woba(
					player,
					startDate,
					endDate);
				if (woba >= threshold)
					hotList.Add(player);
			}
			return hotList;
		}

		private List<string> FilterOutSignedPlayers(
			List<string> rostered)
		{
			var freeAgents = new List<string>();
			foreach (var player in rostered)
			{
				if (_rosterMaster
					.GetOwnerOf(player).Equals("FA"))
				{
					freeAgents.Add(player);
				}
			}
			return freeAgents;
		}
	}
}
