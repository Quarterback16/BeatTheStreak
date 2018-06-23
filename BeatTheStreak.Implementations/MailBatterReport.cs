using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using BeatTheStreak.Models;


namespace BeatTheStreak.Implementations
{
	public class MailBatterReport : IMailBatterReport
	{
		private readonly IMailMan _mailMan;
		private readonly ILog _logger;

		public MailBatterReport(IMailMan mailMan, ILog logger)
		{
			_mailMan = mailMan;
			_logger = logger;
		}

		public void MailReport(BatterReport report)
		{
			var message = report.AsString();
			_logger.Info(message);
			_mailMan.SendMail(
				message, 
				subject: $"BTS {Utility.UniversalDate(report.GameDate)}");
			_logger.Info($"mail sent to {_mailMan.RecipientCsv()}");
		}
	}
}
