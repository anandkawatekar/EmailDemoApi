using AutoMapper;
using MailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailService.Services
{
    public class MailAttachmentService
    {
        private MailManagerDBConnection dataContext = new MailManagerDBConnection();

        public dtoMailAttachment GetAttachment(decimal id)
        {
            try
            {
                var Attachment = dataContext.MailAttachments.Find(id);

                dtoMailAttachment dtoAttachment = Mapper.Map<dtoMailAttachment>(Attachment);

                return dtoAttachment;
            }
            catch (Exception ex)
            {
                throw ex; ;
            }
        }

        public Int32 SaveAttachment(dtoMailAttachment eMailAttach)
        {
            try
            {
                MailAttachment attch = Mapper.Map<MailAttachment>(eMailAttach);
                dataContext.MailAttachments.Add(attch);
                int retValue = dataContext.SaveChanges();
                dataContext = new MailManagerDBConnection();
                return attch.AttachmentId;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DeleteAttachment(Int32 id)
        {
            try
            {
                var attch = dataContext.MailAttachments.Find(id);
                if (attch == null)
                {
                    throw new Exception("Attachment does not exits in database.");
                }

                dataContext.MailAttachments.Remove(attch);
                Int32 retval = dataContext.SaveChanges();
                if (retval > 0)
                {
                    // remove from physical path
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}