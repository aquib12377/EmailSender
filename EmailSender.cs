
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
        public static EmailResponse SendEmail(string senderEmail, SecureString senderPassword, List<string> receiverEmails, string body = "Test Body", string subject = "Test Email", List<string> files = null)
        {
            bool fileExists = false;
            List<Attachment> attachments = null;
            try
            {
                
                if (!IsEmailValid(senderEmail) || receiverEmails.Any(x => !IsEmailValid(x)) && receiverEmails != null)
                {
                    throw new Exception("Email is not valid");
                }
                attachments = files != null && files.Count > 0 ? new List<Attachment>() : null;

                if(files != null)
                    foreach (var file in files)
                    {
                        if (!string.IsNullOrEmpty(file))
                        {
                            if (!File.Exists(file))
                                throw new FileNotFoundException("File does not exist at given path", file);
                            else
                            {
                                fileExists = true;
                                Attachment attachment = new Attachment(File.OpenRead(file), file.Split("\\").LastOrDefault());
                                attachments.Add(attachment);
                            }
                        }
                    }

                MailMessage message = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = subject,
                    IsBodyHtml = true,
                    Body = body
                };
                foreach (var receiver in receiverEmails)
                {
                    message.To.Add(new MailAddress(receiver));
                }
                
                //Attachments binding
                foreach (var attachment in attachments??new List<Attachment>())
                {
                    message.Attachments.Add(attachment);
                }

                SmtpClient smtp = new SmtpClient
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                smtp.Send(message);
                return !fileExists ? new EmailResponse(true, "Successfully sent Email.", null) 
                    : new EmailResponse(true, "Successfully sent Email.", null,attachments.Count);
            }
            catch (Exception ex)
            {
                return !fileExists ? new EmailResponse(false, "Mail not sent.", ex)
                    : new EmailResponse(false, "Mail not sent.", ex, attachments.Count);
            }
        }

        private static bool IsEmailValid(string email)
            => Regex.IsMatch(email,
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase);
    }
}
