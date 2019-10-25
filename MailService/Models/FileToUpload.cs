using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailService.Models
{
    public class FileToUpload
    {
        public string FileName { get; set; }
        public Int32 FileSize { get; set; }
        public string FileType { get; set; }
        public string FileAsBase64 { get; set; }
        public byte[] FileAsByteArray { get; set; }
        public string FolderName { get; set; }
    }
}