using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MailService.Models;

namespace MailService.Services
{
    public class FileService
    {
        private MailManagerDBConnection dataContext = new MailManagerDBConnection();

        private MailAttachmentService mailAttachmentService = new MailAttachmentService();

        string filePath = HttpContext.Current.Server.MapPath("~/UploadEmailAttachments/");

        public Int32 UploadFile(FileToUpload file)
        {
            try
            {
                string newPath = Path.Combine(filePath, file.FolderName);
                if (!Directory.Exists(newPath))
                {
                    Directory.CreateDirectory(newPath);
                }
                string fullPath = Path.Combine(newPath, file.FileName);
                file.FileAsByteArray = Convert.FromBase64String(file.FileAsBase64);

                using (var fs = new FileStream(fullPath, FileMode.Create))
                {
                    fs.Write(file.FileAsByteArray, 0,file.FileAsByteArray.Length);
                }

                dtoMailAttachment dtoA = new dtoMailAttachment();
                dtoA.Attachment = file.FileName;
                dtoA.MailId = Convert.ToInt32(file.FolderName);
                return mailAttachmentService.SaveAttachment(dtoA);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public bool DeleteFile(Int32 id)
        {
            try
            {
                var attch = mailAttachmentService.GetAttachment(id);
                if (attch == null)
                {
                    throw new Exception("Attachment does not exits.");
                }

                //delete physical file
                bool retVal = mailAttachmentService.DeleteAttachment(id);

                if(retVal)
                {
                    try
                    {
                        string newPath = Path.Combine(filePath, attch.MailId.ToString());
                        string fileToDelete = Path.Combine(filePath, attch.Attachment);
                        File.Delete(fileToDelete);
                    }
                    catch (Exception ee)
                    { }
                }

                return retVal;


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeleteAllMailAttachments(Int32 MailId)
        {
            try
            {
                string deleteFilePath = Path.Combine(filePath, MailId.ToString());

                if (!Directory.Exists(deleteFilePath))
                {
                    Directory.Delete(deleteFilePath);
                }
            }
            catch (Exception ex)
            { }
            
        }

        public void CopyFiles(Int32 sourceMailId,Int32 destMailId)
        {
            try
            {
                string sourceFilePath = Path.Combine(filePath, sourceMailId.ToString());
                string destinationFilePath = Path.Combine(filePath, destMailId.ToString());

                string[] filesList = Directory.GetFiles(sourceFilePath);
                foreach (var fileName in filesList)
                {
                    string newFilePath = fileName.ToString().Replace(sourceFilePath, destinationFilePath);
                    if (!Directory.Exists(destinationFilePath))
                    {
                        Directory.CreateDirectory(destinationFilePath);
                    }

                    File.Copy(fileName, newFilePath,true);

                }

            }
            catch (Exception ex)
            { }

        }


        //public bool DownloadFile(Int32 id)
        //{
        //    try
        //    {
        //        var attch = mailAttachmentService.GetAttachment(id);
        //        if (attch == null)
        //        {
        //            throw new Exception("Attachment does not exits.");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
    }
}