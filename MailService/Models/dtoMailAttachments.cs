﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailService.Models
{
    public class dtoMailAttachment
    {
        public dtoMailAttachment()
        { }

        public int AttachmentId { get; set; }
        public Nullable<int> MailId { get; set; }
        public string Attachment { get; set; }
    }
}