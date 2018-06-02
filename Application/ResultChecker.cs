using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;
using System;

namespace Application
{
	public class ResultChecker
	{
		private readonly IGameLogRequest _gameLogRequest;

		public ResultChecker(IGameLogRequest gameLogRequest)
		{
			_gameLogRequest = gameLogRequest;
		}

		public void Check( BatterReport batterReport )
		{
			if ( batterReport.GameDate < DateTime.Now.AddDays(-2) )
			{
				foreach (var selection in batterReport.Selections)
				{
					var result = _gameLogRequest.Submit(
						queryDate: batterReport.GameDate,
						playerSlug: selection.Batter.PlayerSlug);
					result.Dump();
				}
			}
			else
				Console.WriteLine("Game too fresh");
		}
	}
}
