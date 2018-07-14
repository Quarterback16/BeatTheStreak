using BeatTheStreak.Helpers;
using System.Collections.Generic;

namespace BeatTheStreak.Interfaces
{
    public interface IMailMan
    {
        Result SendMail(string message, string subject);

        Result SendMail(string message, string subject, string attachment);

        Result SendMail(string message, string subject, string[] attachments);

        void AddRecipients(List<string> recipients);

        void AddRecipients(string recipients);

        int RecipientCount();

		string RecipientCsv();

		void ClearRecipients();
    }
}