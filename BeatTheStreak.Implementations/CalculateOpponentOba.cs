using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using System;

namespace BeatTheStreak.Implementations
{
	public class CalculateOpponentOba : ICalculateOpponentOba
	{
		private readonly ILog _logger;
		private readonly IGameLogRepository _gameLogRepository;

		public CalculateOpponentOba(
			ILog logger,
			IGameLogRepository gameLogRepository)
		{
			_logger = logger;
			_gameLogRepository = gameLogRepository;
		}

		public decimal CalculateOba(
			string playerSlug,
			DateTime gameDate,
			int daysBack)
		{
			if (string.IsNullOrEmpty(playerSlug))
				return -1.000M;
			DateTime focusDate = new DateTime(1,1,1);
			try
			{
				var totalHits = 0;
				var totalOuts = 0;
				for (int i = 1; i < daysBack + 1; i++)
				{
					focusDate = gameDate.AddDays(-i);
					var gameLog = _gameLogRepository.Submit(
									queryDate: focusDate,
									playerSlug: playerSlug);

					if (!gameLog.GameStarted)
						continue;

					_logger.Trace(
						$@" on {
							focusDate.ToShortDateString()
							} {
							gameLog.PitcherLine()
							}");
					totalHits += gameLog.HitsAllowed;
					totalOuts += gameLog.OutsRecorded;
				}
				var oba = Utility.BattingAverage(
					totalHits, totalOuts + totalHits);
				return oba;
			}
			catch (Exception ex)
			{
				_logger.Error($"{playerSlug} : {focusDate.ToShortDateString()}");
				_logger.Error(ex.Message);
				throw;
			}
		}
	}
}
