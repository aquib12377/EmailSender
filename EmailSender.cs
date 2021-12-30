
using System;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;

namespace EmailSender
{
    public class EmailSender
    {
        public static bool SendEmail(string senderEmail,string senderPassword,string receiverEmail,string body = "",string subject = "")
        {
            try
            {
                if(!IsEmailValid(senderEmail) || !IsEmailValid(receiverEmail))
                {
                    throw new Exception("Email is not valid");
                }
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(senderEmail);
                message.To.Add(new MailAddress(receiverEmail));
                message.Subject = subject;
                message.IsBodyHtml = true; 
                message.Body = body;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static bool IsEmailValid(string email) 
            => Regex.IsMatch(email,
                @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
                RegexOptions.IgnoreCase);
    }
}
