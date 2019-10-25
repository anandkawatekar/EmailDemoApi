using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MailService.Models;
using MailService.Services;

namespace MailService.Controllers
{
    [RoutePrefix("api/Mails")]
    public class MailsController : ApiController
    {
        private MailMessageService mailMessageService = new MailMessageService();

        // GET: api/Mails/Inbox?eMailId
        [HttpGet]
        [Route("Inbox")]
        public IHttpActionResult GetInboxMails(string eMailId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                List<dtoMailMessage> InboxMails = mailMessageService.GetInboxMails(eMailId);

                if (InboxMails == null)
                {
                    InboxMails = new List<dtoMailMessage>();
                }

                return Ok(InboxMails);

            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }

        // GET: api/Mails/Sent?eMailId
        [HttpGet]
        [Route("Sent")]
        public IHttpActionResult GetSentMails(string eMailId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                List<dtoMailMessage> sentMails = mailMessageService.GetSentMails(eMailId);

                if (sentMails == null)
                {
                    sentMails = new List<dtoMailMessage>();
                }

                return Ok(sentMails);

            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }

        // GET: api/Mails/Draft?eMailId
        [HttpGet]
        [Route("Draft")]
        public IHttpActionResult GetDraftMails(string eMailId)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);
                }

                List<dtoMailMessage> draftMails = mailMessageService.GetDraftMails(eMailId);

                if (draftMails == null)
                {
                    draftMails = new List<dtoMailMessage>();
                }

                return Ok(draftMails);

            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }

        // GET: api/Mails/Trash?eMailId
        [HttpGet]
        [Route("Trash")]
        public IHttpActionResult GetTrashMails(string eMailId)
        {
            try
            {
                if (!ModelState.IsValid)
                {

                    return BadRequest(ModelState);
                }

                List<dtoMailMessage> draftMails = mailMessageService.GetTrashMails(eMailId);

                if (draftMails == null)
                {
                    draftMails = new List<dtoMailMessage>();
                }

                return Ok(draftMails);

            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }

        // GET: api/Mails/2
        [HttpGet]
        public IHttpActionResult GetMail(Int32 Id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                dtoMailMessage mailDetail = mailMessageService.GetMailDetails(Id);

                if (mailDetail == null)
                {
                    return NotFound();
                }

                return Ok(mailDetail);

            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }

        // POST: api/Mails      //To Send Email
        [HttpPost]
        public IHttpActionResult PostMail([FromBody] dtoMailMessage mailToSend)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(mailMessageService.SendMail(mailToSend));

            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }

        // PUT/POST: api/Mails      //To Draft Email
        [HttpPut, HttpPost]
        public IHttpActionResult PutMail(Int32 Id, [FromBody] dtoMailMessage mailToSave)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

               return Ok(mailMessageService.SaveDraft(mailToSave));

            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }

        // PUT: api/Mails/IsRead    //To Draft Email
        [HttpPut]
        [Route("SetReadStatus/{id}")]
        public IHttpActionResult SetMailReadStatus([FromUri] Int32 Id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(mailMessageService.SetReadStatus(Id));

            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }

        // DELETE: api/Mails      //To Delete Email
        [HttpDelete]
        public IHttpActionResult DeleteMail(Int32 id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                return Ok(mailMessageService.DeleteMail(id));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Email does not exits"))
                {
                    return NotFound();
                }
                return InternalServerError(new Exception(ex.Message));
            }
        }

        // DELETE: api/Mails/DoTrash      //To Trash Email
        [HttpDelete]
        [Route("DoTrash/{id}")]
        public IHttpActionResult DoTrash(Int32 id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                return Ok(mailMessageService.DoTrash(id));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Email does not exits"))
                {
                    return NotFound();
                }
                return InternalServerError(new Exception(ex.Message));
            }
        }

        
    }
}