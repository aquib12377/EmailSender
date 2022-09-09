using System;
using System.Collections.Generic;
using System.Security;

namespace EmailSender
{
    class Program
    {
        private static string SenderEmail = string.Empty;
        private static string Subject = string.Empty;
        private static string Body = string.Empty;
        private static SecureString SenderPassword = null;
        private static List<string> ReceiverEmails = null;
        private static List<string> Files = null;
        static void Main(string[] args)
        {
            SenderEmail = "Sender email";
            SenderPassword = new SecureString();

            foreach (var _char in "Sender password")
            {
                SenderPassword.AppendChar(_char);
            }

            Subject = "Test Email";
            Body = "Test Body";
            ReceiverEmails = new List<string>
            {
                "Receiver Emails",
            };
            //Send email without attachment
            var res1 = EmailSender.SendEmail(SenderEmail, SenderPassword, ReceiverEmails, Body, Subject);

            Files = new List<string>
            {
                @"attachment path"
            };
            //Send Email with attachments
            var res2 = EmailSender.SendEmail(SenderEmail, SenderPassword, ReceiverEmails, Body, Subject, Files);
        }
    }
}
