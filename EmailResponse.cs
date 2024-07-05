using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSender
{
    public class EmailResponse
    {
        public bool Status { get; }
        public string Message { get; }
        public object ResponseObject { get; }

        public EmailResponse(bool status, string message, object responseObject)
        {
            Status = status;
            Message = message;
            ResponseObject = responseObject;
        }

        public static EmailResponse WithAttachments(bool status, string message, object responseObject, int attachmentsCount)
        {
            string messageWithAttachments = $"{message}\r\nAlong with {attachmentsCount} attachments.";
            return new EmailResponse(status, messageWithAttachments, responseObject);
        }
    }

}
