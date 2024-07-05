
using System;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Security;

namespace EmailSender
{
    public class EmailSender
    {
        /// <summary>
        /// Sends an email with the specified parameters and returns an EmailResponse indicating the result.
        /// </summary>
        /// <param name="senderEmail">The email address of the sender.</param>
        /// <param name="senderPassword">The password of the sender's email account, as a SecureString.</param>
        /// <param name="receiverEmails">A list of receiver email addresses.</param>
        /// <param name="body">The body of the email. Defaults to "Test Body" if not provided.</param>
        /// <param name="subject">The subject of the email. Defaults to "Test Email" if not provided.</param>
        /// <param name="files">A list of file paths for attachments. Defaults to null if not provided.</param>
        /// <returns>An EmailResponse object indicating the success or failure of the email sending process.</returns>
        /// <exception cref="ArgumentException">Thrown if the sender's email or any of the receiver's emails are not valid.</exception>
        /// <exception cref="FileNotFoundException">Thrown if any of the provided file paths do not exist.</exception>
        /// <exception cref="Exception">Thrown if any other errors occur during the email sending process.</exception>
        public static EmailResponse SendEmail(string senderEmail, SecureString senderPassword, List<string> receiverEmails, string body = "Test Body", string subject = "Test Email", List<string> files = null)
        {
            try
            {
                // Validate the sender's email and all receiver emails.
                ValidateEmails(senderEmail, receiverEmails);

                // Prepare the attachments if any files are provided.
                List<Attachment> attachments = GetAttachments(files);

                // Create the MailMessage object with the provided parameters.
                MailMessage message = CreateMailMessage(senderEmail, receiverEmails, body, subject, attachments);

                // Send the email using the SmtpClient.
                SendMailMessage(senderEmail, senderPassword, message);

                // Return a successful EmailResponse, indicating if attachments were included.
                return attachments == null || attachments.Count == 0
                    ? new EmailResponse(true, "Successfully sent Email.", null)
                    : EmailResponse.WithAttachments(true, "Successfully sent Email.", null, attachments.Count);
            }
            catch (Exception ex)
            {
                // Determine if any attachments exist to include in the failure response.
                bool attachmentsExist = files != null && files.Any(file => File.Exists(file));
                return !attachmentsExist
                    ? new EmailResponse(false, "Mail not sent.", ex)
                    : EmailResponse.WithAttachments(false, "Mail not sent.", ex, files.Count);
            }
        }


        private static void ValidateEmails(string senderEmail, List<string> receiverEmails)
        {
            if (!IsEmailValid(senderEmail) || receiverEmails == null || receiverEmails.Any(email => !IsEmailValid(email)))
            {
                throw new ArgumentException("One or more emails are not valid.");
            }
        }

        private static List<Attachment> GetAttachments(List<string> files)
        {
            if (files == null || files.Count == 0) return null;

            List<Attachment> attachments = new List<Attachment>();
            foreach (var file in files)
            {
                if (!string.IsNullOrEmpty(file))
                {
                    if (!File.Exists(file))
                    {
                        throw new FileNotFoundException("File does not exist at the given path", file);
                    }
                    else
                    {
                        Attachment attachment = new Attachment(File.OpenRead(file), Path.GetFileName(file));
                        attachments.Add(attachment);
                    }
                }
            }

            return attachments;
        }

        private static MailMessage CreateMailMessage(string senderEmail, List<string> receiverEmails, string body, string subject, List<Attachment> attachments)
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress(senderEmail),
                Subject = subject,
                IsBodyHtml = true,
                Body = body
            };

            receiverEmails.ForEach(email => message.To.Add(new MailAddress(email)));

            attachments?.ForEach(attachment => message.Attachments.Add(attachment));

            return message;
        }

        private static void SendMailMessage(string senderEmail, SecureString senderPassword, MailMessage message)
        {
            using SmtpClient smtp = new SmtpClient
            {
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                DeliveryMethod = SmtpDeliveryMethod.Network
            };
            smtp.Send(message);
        }

        private static bool IsEmailValid(string email)
            => Regex.IsMatch(email,
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase);

    }
}
