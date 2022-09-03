using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSender
{
    public class EmailResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public object ResponseObject { get; set; }

        public EmailResponse(bool status, string message, object responseObject)
        {
            Status = status;
            Message = message;
            ResponseObject = responseObject;
        }

        public EmailResponse(bool status, string message, object responseObject, int attachmentsCount)
        {
            Status = status;
            Message = message + "\r\n" + $"Along with {attachmentsCount} attachments.";
            ResponseObject = responseObject;
        }
    }
}
