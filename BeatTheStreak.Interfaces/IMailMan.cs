using System.Collections.Generic;

namespace BeatTheStreak.Interfaces
{
    public interface IMailMan
    {
        string SendMail(string message, string subject);

        string SendMail(string message, string subject, string attachment);

        string SendMail(string message, string subject, string[] attachments);

        void AddRecipients(List<string> recipients);

        void AddRecipients(string recipients);

        int RecipientCount();

		string RecipientCsv();

		void ClearRecipients();
    }
}