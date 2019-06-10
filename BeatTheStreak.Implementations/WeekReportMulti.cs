using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using Domain;
using FbbEventStore;
using System;
using System.Collections.Generic;

namespace BeatTheStreak.Implementations
{
	public class WeekReportMulti : PlayerReport
	{
		private readonly IGameLogRepository _gameLogRepository;
		private readonly IRosterMaster _rosterMaster;
		private readonly string[] _players;
		private readonly int _week;

		public List<Player> Players { get; set; }

		public bool IncludePriorWeek { get; set; }

		public WeekReportMulti(
			IGameLogRepository gameLogRepository,
			IRosterMaster rosterMaster,
			string[] players,
			int week)
		{
			_gameLogRepository = gameLogRepository;
			_rosterMaster = rosterMaster;
			_players = players;
			Players = AsPlayers(players);
			_week = week;
		}

		private List<Player> AsPlayers(string[] players)
		{
			Players = new List<Player>();
			foreach (var item in players)
			{
				Players.Add(
					new Player
					{
						Name = item
					});
			}
			return Players;
		}

		public WeekReportMulti(
			IGameLogRepository gameLogRepository,
			IRosterMaster rosterMaster,
			List<HotListViewModel> players,
			int week)
		{
			_gameLogRepository = gameLogRepository;
			_rosterMaster = rosterMaster;
			_players = AsPlayerNames(players);
			Players = AsPlayers(players);
			_week = week;
		}

		private List<Player> AsPlayers(List<HotListViewModel> players)
		{
			Players = new List<Player>();
			foreach (var item in players)
			{
				Players.Add(
					new Player
					{
						Name = item.Player.Name,
						Slug = item.Player.Slug
					});
			}
			return Players;
		}

		private string[] AsPlayerNames(List<HotListViewModel> players)
		{
			var playerArray = new string[players.Count];
			var index = 0;
			foreach (var player in players)
			{
				playerArray[index] = player.Player.Name;
				index++;
			}
			return playerArray;
		}

		public void Execute()
		{
			SetOutput();
			var i = 0;
			foreach (var player in Players)
			{
				var sut = new WeekReport(
					_gameLogRepository,
					_rosterMaster)
				{
					WeekStarts = Utility.WeekStart(_week),
					Player = player.Name,
					PlayerSlug = player.Slug,
					Hitters = true,
					IncludePriorWeek = IncludePriorWeek
				};
				sut.DumpWeek(++i);
			}
			CloseOutput();
		}

	}
}
