using AutoMapper;
using MailService.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MailService.Services
{
    public class MailMessageService
    {
        private MailManagerDBConnection dataContext = new MailManagerDBConnection();

        private FileService fileService = new FileService();

        public MailMessageService()
        {
        }

        public List<dtoMailMessage> GetInboxMails( string  eMailId)
        {

            var inboxmails =  dataContext.Mails.Where(x => x.ToEmail == eMailId && x.EmailStatus == "RECEIVED" && x.MailFolder == "INBOX").OrderByDescending(t => t.MailDate).ToList();

            List<dtoMailMessage> dtoInboxMails = Mapper.Map<List<dtoMailMessage>>(inboxmails);

            return dtoInboxMails;

        }

        public List<dtoMailMessage> GetSentMails(string eMailId)
        {
            var inboxmails = dataContext.Mails.Where(x => x.FromEmail == eMailId && x.EmailStatus == "SENT" && x.MailFolder == "SENT").OrderByDescending(t => t.MailDate).ToList();

            List<dtoMailMessage> dtoInboxMails = Mapper.Map<List<dtoMailMessage>>(inboxmails);

            return dtoInboxMails;
        }

        public List<dtoMailMessage> GetDraftMails(string eMailId )
        {
            var inboxmails = dataContext.Mails.Where(x => x.FromEmail == eMailId && x.EmailStatus == "DRAFT" && x.MailFolder == "DRAFT").OrderByDescending(t => t.MailDate).ToList();

            List<dtoMailMessage> dtoInboxMails = Mapper.Map<List<dtoMailMessage>>(inboxmails);

            return dtoInboxMails;
        }

        public List<dtoMailMessage> GetTrashMails(string eMailId)
        {
            var sentmails = dataContext.Mails.Where(x => x.FromEmail == eMailId && x.EmailStatus == "SENT" && x.MailFolder == "TRASH").ToList();

            var inboxmails = dataContext.Mails.Where(x => x.ToEmail == eMailId && x.EmailStatus == "RECEIVED" && x.MailFolder == "TRASH").ToList();

            var trashmails = sentmails.Union(inboxmails).OrderByDescending(t=>t.MailDate).ToList();

            List<dtoMailMessage> dtoInboxMails = Mapper.Map<List<dtoMailMessage>>(trashmails);

            return dtoInboxMails;
        }

        public dtoMailMessage GetMailDetails(Int32 mailId)
        {
            var mailDetail = dataContext.Mails.Where(m => m.MailId == mailId).FirstOrDefault();

            if (mailDetail == null)
            {
                return null;
            }

            dtoMailMessage dtoMail = Mapper.Map<dtoMailMessage>(mailDetail);
            dtoMail.FromUser = Mapper.Map<dtoUserAccount>(dataContext.UserAccounts.Where(u => u.EmailId == dtoMail.FromEmail).FirstOrDefault());
            string[] tm = dtoMail.ToEmail.Split(',');
            dtoMail.ToUsersList = Mapper.Map<List<dtoUserAccount>>(dataContext.UserAccounts.Where(u2 => tm.Contains(u2.EmailId)).ToList());
            dtoMail.AttachmentsList = Mapper.Map<List<dtoMailAttachment>>(dataContext.MailAttachments.Where(a1 => a1.MailId == dtoMail.MailId).ToList());
            return dtoMail;
        }

        public dtoMailTransactionResult SendMail(dtoMailMessage eMail)
        {
            try
            {
                dtoMailTransactionResult mailResp = new dtoMailTransactionResult();
                Mail mailMessage = new Mail();
                bool senderMailUpdateStatus = false;

                eMail.MailId = validateMailForDraft(eMail);

                var emailsToSend = eMail.ToEmail.Split(',');

                foreach (string toEmail in emailsToSend)
                {   
                    mailMessage = Mapper.Map<Mail>(eMail); //mapping 

                    //prepare eMail Message for Reveiver
                    PrepareReceiverMailData(mailMessage, toEmail);

                    MailStatus mailStatus = new MailStatus();
                    mailStatus.EmailId = toEmail;
                    mailStatus.IsSentSuccessful = false;
                    using (DbContextTransaction transaction = dataContext.Database.BeginTransaction())
                    {
                        try
                        {
                            //Save reveiver Mail
                            dataContext.Mails.Add(mailMessage);
                            int resCount = dataContext.SaveChanges();

                            foreach (var dtoAttach in eMail.AttachmentsList)
                            {
                                MailAttachment mAttach = new MailAttachment();
                                mAttach.AttachmentId = 0;
                                mAttach.MailId = mailMessage.MailId;
                                mAttach.Attachment = dtoAttach.Attachment;

                                dataContext.MailAttachments.Add(mAttach);
                                int attRes = dataContext.SaveChanges();
                            }

                            mailStatus.MailId = mailMessage.MailId; //
                            mailStatus.IsSentSuccessful = true;
                            mailResp.Mails.Add(mailStatus);

                            if (!senderMailUpdateStatus)
                            {
                                Mail senderMail = Mapper.Map<Mail>(eMail); //mapping
                                PrepareSenderMailData(senderMail);
                                //Update sender mail Status
                                dataContext.Entry(senderMail).State = EntityState.Modified;
                                Int32 senderReturnVal = dataContext.SaveChanges();
                                senderMailUpdateStatus = true;
                                mailResp.ResultSattus = true;
                            }
    
                            transaction.Commit();

                            fileService.CopyFiles(eMail.MailId, mailMessage.MailId);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            mailStatus.IsSentSuccessful = false;
                            mailResp.Mails.Add(mailStatus);
                        }
                    }
                }
                
                return mailResp;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public Int32 SaveDraft(dtoMailMessage eMail)
        {
            try
            {
                Mail mail = Mapper.Map<Mail>(eMail);
                PrepareDraftMailData(mail);

                if (eMail.MailId == 0)
                {
                    dataContext.Mails.Add(mail);
                    int retValue = dataContext.SaveChanges();
                    dataContext = new MailManagerDBConnection();
                    
                    return mail.MailId;
                }
                else
                {
                    dataContext.Entry(mail).State = EntityState.Modified;
                    Int32 retval = dataContext.SaveChanges();
                    dataContext = new MailManagerDBConnection();
                    return mail.MailId;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool SetReadStatus(Int32  mailId)
        {
            try
            {
                var mail = dataContext.Mails.Find(mailId);
                if (mail == null)
                {
                    throw new Exception("Email does not exits in database.");
                }

                mail.IsRead = true;

                dataContext.Entry(mail).State = EntityState.Modified;
                //Specify the fields that should not be updated.
                dataContext.Entry(mail).Property(x => x.MailDate).IsModified = false;
                dataContext.Entry(mail).Property(x => x.FromEmail).IsModified = false;
                dataContext.Entry(mail).Property(x => x.ToEmail).IsModified = false;
                dataContext.Entry(mail).Property(x => x.Subject).IsModified = false;
                dataContext.Entry(mail).Property(x => x.Message).IsModified = false;
                dataContext.Entry(mail).Property(x => x.IsAttachmentPresent).IsModified = false;
                dataContext.Entry(mail).Property(x => x.EmailStatus).IsModified = false;
                dataContext.Entry(mail).Property(x => x.MailFolder).IsModified = false;

                Int32 retval = dataContext.SaveChanges();
                if (retval > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DoTrash(Int32 mailid)
        {
            try
            {
                var mail = dataContext.Mails.Find(mailid);
                if (mail == null)
                {
                    throw new Exception("Email does not exits in database.");
                }

                mail.MailFolder = "TRASH";

                dataContext.Entry(mail).State = EntityState.Modified;
                //Specify the fields that should not be updated.
                dataContext.Entry(mail).Property(x => x.MailDate).IsModified = false;
                dataContext.Entry(mail).Property(x => x.FromEmail).IsModified = false;
                dataContext.Entry(mail).Property(x => x.ToEmail).IsModified = false;
                dataContext.Entry(mail).Property(x => x.Subject).IsModified = false;
                dataContext.Entry(mail).Property(x => x.Message).IsModified = false;
                dataContext.Entry(mail).Property(x => x.IsAttachmentPresent).IsModified = false;
                dataContext.Entry(mail).Property(x => x.EmailStatus).IsModified = false;
                dataContext.Entry(mail).Property(x => x.IsRead).IsModified = false;

                Int32 retval = dataContext.SaveChanges();
                if (retval > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public bool DeleteMail(Int32 mailid)
        {
            try
            {
                var mail = dataContext.Mails.Find(mailid);
                if (mail == null)
                {
                    throw new Exception("Email does not exits in database.");
                }
                
                dataContext.Mails.Remove(mail);
                Int32 retval = dataContext.SaveChanges();
                if (retval > 0)
                {
                    fileService.DeleteAllMailAttachments(mailid);
                    return true;
                }
                else
                {
                    return false;
                } 
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        private Int32 validateMailForDraft(dtoMailMessage eMail)
        {
            if (eMail.MailId > 0)
            {
                return eMail.MailId;
            }

            return SaveDraft(eMail);
        }

        private void PrepareDraftMailData(Mail mail)
        {
            try
            {
                mail.MailDate = System.DateTime.Now;
                mail.MailFolder = "DRAFT";
                mail.EmailStatus = "DRAFT";
                mail.IsRead = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PrepareReceiverMailData(Mail mailMessage, string receiverEmailId)
        {
            try
            {
                mailMessage.MailId = 0;
                mailMessage.ToEmail = receiverEmailId;
                mailMessage.MailDate = System.DateTime.Now;
                mailMessage.EmailStatus = "RECEIVED";
                mailMessage.MailFolder = "INBOX";
                mailMessage.IsRead = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void PrepareSenderMailData(Mail mailMessage)
        {
            try
            {
                //mailMessage.MailId = 0;
                mailMessage.MailDate = System.DateTime.Now;
                mailMessage.EmailStatus = "SENT";
                mailMessage.MailFolder = "SENT";
                mailMessage.IsRead = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}