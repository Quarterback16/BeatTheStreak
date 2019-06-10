using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Domain;
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

		public List<HotListViewModel> GetHotList(
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

		private List<HotListViewModel> FilterOutPlayersBelow(
			decimal threshold, 
			List<Player> freeAgents,
			DateTime startDate,
			DateTime endDate)
		{
			var hotList = new List<HotListViewModel>();
			foreach (var player in freeAgents)
			{
				var woba = _statCalculator.WobaBySlug(
					player.Slug,
					startDate,
					endDate);
				var ab = _statCalculator.AbBySlug(
					player.Slug,
					startDate,
					endDate);
				var abCap = AbsToQualify(startDate, endDate);
				if (woba >= threshold && ab > abCap)
					hotList.Add(ViewModelFor(player, woba, ab));
			}
			return hotList;
		}

		private int AbsToQualify(
			DateTime startDate,
			DateTime endDate)
		{
			TimeSpan difference = endDate - startDate;
			return (int) (difference.TotalDays + 1) * 2;
		}

		private HotListViewModel ViewModelFor(
			Player player,
			decimal woba,
			decimal ab)
		{
			var vm = new HotListViewModel
			{
				Player = player,
				Woba = woba,
				AtBats = ab
			};
			return vm;
		}

		private List<Player> FilterOutSignedPlayers(
			List<Player> rostered)
		{
			var freeAgents = new List<Player>();
			foreach (var player in rostered)
			{
				if (_rosterMaster
					.GetOwnerOf(player.Name).Equals("FA"))
				{
					freeAgents.Add(player);
				}
			}
			return freeAgents;
		}
	}
}
