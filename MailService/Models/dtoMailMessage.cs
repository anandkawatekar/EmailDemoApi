using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailService.Models
{
    public class dtoMailMessage
    {
        public dtoMailMessage()
        { }
        public int MailId { get; set; }
        public Nullable<System.DateTime> MailDate { get; set; }
        public string FromEmail { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public Nullable<bool> IsAttachmentPresent { get; set; }
        public string EmailStatus { get; set; }
        public string MailFolder { get; set; }
        public Nullable<bool> IsRead { get; set; }

        public dtoUserAccount FromUser = new dtoUserAccount();
        public List<dtoUserAccount> ToUsersList = new List<dtoUserAccount>();

        public List<dtoMailAttachment> AttachmentsList = new List<dtoMailAttachment>();
    }
}