using System;
using System.Collections.Generic;
using System.Security;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EmailSender
{
    class Program
    {
        private static readonly string SenderEmail = "Sender email";
        private static readonly SecureString SenderPassword = GetSecureString("Sender password");
        private static readonly string Subject = "Test Email";
        private static readonly string Body = "Test Body";
        private static readonly List<string> ReceiverEmails = new List<string> { "Receiver Emails" };
        private static readonly List<string> Files = new List<string> { @"attachment path" };

        static void Main(string[] args)
        {
            // Send email without attachment
            var res1 = EmailSender.SendEmail(SenderEmail, SenderPassword, ReceiverEmails, Body, Subject);

            Console.WriteLine($"Email response without attachments -> {JsonSerializer.Serialize(res1)}");

            // Send email with attachments
            var res2 = EmailSender.SendEmail(SenderEmail, SenderPassword, ReceiverEmails, Body, Subject, Files);

            Console.WriteLine($"Email response with attachments -> {JsonSerializer.Serialize(res2)}");
        }

        private static SecureString GetSecureString(string plainText)
        {
            SecureString secureString = new SecureString();
            foreach (char c in plainText)
            {
                secureString.AppendChar(c);
            }
            secureString.MakeReadOnly();
            return secureString;
        }
    }
}
