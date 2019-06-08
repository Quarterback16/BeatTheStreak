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

		public HotList(
			IActualRoster actualRoster,
			IRosterMaster rosterMaster)
		{
			_actualRoster = actualRoster;
			_rosterMaster = rosterMaster;
		}

		public List<string> GetHotList(
			List<string> teamSlugs,
			DateTime queryDate, 
			int gamesBack)
		{
			var hotList = new List<string>();

			var rostered = _actualRoster.GetActualRoster(
				teamSlugs,
				queryDate,
				gamesBack,
				battersOnly: true);

			var freeAgents = FilterOutSignedPlayers(
				rostered);
			return hotList;
		}

		private object FilterOutSignedPlayers(
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
