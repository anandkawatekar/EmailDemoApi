using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailService.Models
{
    public class dtoMailTransactionResult
    {
        public bool ResultSattus { get; set; }

        public List<MailStatus> Mails = new List<MailStatus>();
    }

    public class MailStatus
    {
        public string EmailId { get; set; }
        public Int32 MailId { get; set; }
        public bool IsSentSuccessful { get; set; }
    }
}