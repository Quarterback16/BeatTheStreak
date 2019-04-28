using System;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using BeatTheStreak.Repositories.StattlleShipApi;
using FbbEventStore;

namespace BeatTheStreak.Repositories
{
	public class StartingPitchersRepository : IStartingPitchersRepository
	{
		private readonly IRosterMaster _rosterMaster;

		public StartingPitchersRepository(
			IRosterMaster rosterMaster)
		{
			_rosterMaster = rosterMaster;
		}

		public StartersViewModel Submit(DateTime queryDate)
		{
			var startersRequest = new StarterRequest(
				_rosterMaster);
			var result = startersRequest.Submit(
				queryDate);
			return result;
		}
	}
}
