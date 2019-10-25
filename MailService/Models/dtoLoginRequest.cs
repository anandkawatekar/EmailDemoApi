using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailService.Models
{
    public class dtoLoginRequest
    {
        public string EmailId { get; set; }
        public string Password { get; set; }
    }
}