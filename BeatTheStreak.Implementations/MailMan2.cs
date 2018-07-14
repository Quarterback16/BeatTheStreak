using BeatTheStreak.Helpers;
using BeatTheStreak.Interfaces;
using System.Collections.Generic;
using System.Net.Mail;

namespace BeatTheStreak.Implementations
{
    public class MailMan2 : IMailMan
    {
        private const string K_MailServerKey = "MailServer";
        private const string K_MailUsername = "MailUsername";
        private const string K_MailPassword = "MailPassword";
		private const string K_Recipients = "Recipients";

		public ILog Logger { get; set; }

        public string MailServer { get; set; }
        private string UserId { get; set; }
        private string Password { get; set; }

        public SmtpClient SmtpClient { get; set; }

        private List<string> Recipients { get; set; }

        private IConfigReader ConfigReader { get; set; }

        public MailMan2(IConfigReader configReader,ILog logger)
        {
            ConfigReader = configReader;
            Logger = logger;
            Initialise();
        }

        private Result Initialise()
        {
			Recipients = new List<string>();
			var result = GetMailSettings();
            if (result.IsFailure)
                return result;

            SmtpClient = new SmtpClient(MailServer)
            {
                Port = 465,
                UseDefaultCredentials = false,
                EnableSsl = true,
				Credentials = new System.Net.NetworkCredential(
					userName: UserId,
					password: Password)
			};
            return Result.Ok();
        }

        private Result GetMailSettings()
        {
            MailServer = ConfigReader.GetSetting(K_MailServerKey);
			if (string.IsNullOrEmpty(MailServer))
				return Result.Fail($"Failed to read MailServer setting : {K_MailServerKey}");
            UserId = ConfigReader.GetSetting(K_MailUsername);
            if (string.IsNullOrEmpty(UserId))
                return Result.Fail($"Failed to read MailServer setting : {K_MailUsername}");
            Password = ConfigReader.GetSetting(K_MailPassword);
            if (string.IsNullOrEmpty(Password))
                return Result.Fail($"Failed to read MailServer setting : {K_MailPassword}");
			var csvRecipients = ConfigReader.GetSetting(K_Recipients);
			AddRecipients(csvRecipients);
			return Result.Ok();
        }

        public int RecipientCount()
        {
            return Recipients.Count;
        }

        public MailMan2(List<string> recipients)
        {
            Initialise();
            AddRecipients(recipients);
        }

        public void AddRecipients(List<string> recipients)
        {
            Recipients.AddRange(recipients);
        }

        public void AddRecipients(string recipients)
        {
			if (string.IsNullOrEmpty(recipients))
				return;

            var recipientArray = recipients.Split(',');
            foreach (var item in recipientArray)
            {
                Recipients.Add(item);
            }
        }

        public Result SendMail(string message, string subject)
        {
            try
            {
                using (var mail = new MailMessage())
                {
					mail.Body = message;
                    mail.From = new MailAddress(UserId);
                    foreach (var recipient in Recipients)
                    {
                        mail.To.Add(recipient);
                    }
                    mail.Subject = subject;
                    SmtpClient.Send(mail);
                    Logger.Info($"    mail sent to {mail.To}");
                }
            }
            catch (SmtpException ex)
            {
				Logger.Error(ex.Message);
                return Result.Fail(ex.Message);
            }
            return Result.Ok();
        }

        public Result SendMail(
			string message, 
			string subject, 
			string attachment)
        {
            string[] attachments = new string[1];
            attachments[0] = attachment;
            return SendMail(message, subject, attachments);
        }

        public Result SendMail(
			string message, 
			string subject,
			string[] attachments)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    mail.From = new MailAddress(UserId);
                    foreach (var recipient in Recipients)
                    {
                        mail.To.Add(recipient);
                    }
                    mail.Subject = subject;
                    foreach (string attachment in attachments)
                    {
                        mail.Attachments.Add(new Attachment(attachment));
                    }
                    SmtpClient.Send(mail);
                    Logger.Info($"    mail sent to {mail.To}" );
                }
            }
            catch (SmtpException ex)
            {
				Logger.Error(ex.Message);
                return Result.Fail(ex.Message);
            }

            return Result.Ok();
        }

        public void ClearRecipients()
        {
            Recipients.Clear();
        }

		public string RecipientCsv()
		{
			var csv = string.Empty;
			foreach (var recipient in Recipients)
			{
				csv += recipient + ",";
			}
			return csv;
		}
	}
}