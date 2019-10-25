using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MailService.Models;
using MailService.Services;

namespace MailService.Controllers
{
    //[RoutePrefix("api/Mails/UploadFile")]
    public class FileUploadController : ApiController
    {
        private FileService fileUploadService = new FileService();
        private MailAttachmentService mailAttachmentService = new MailAttachmentService();
        string filePath = HttpContext.Current.Server.MapPath("~/UploadEmailAttachments/");

        // POST: api/FileUpload  
        [HttpPost]
        public IHttpActionResult Post([FromBody] FileToUpload file)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                if (file.FileAsBase64.Contains(","))
                {
                    file.FileAsBase64 = file.FileAsBase64
                      .Substring(file.FileAsBase64
                      .IndexOf(",") + 1);
                }

                return Ok(fileUploadService.UploadFile(file));

            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }
        }

        // DELETE: api/FileUpload       //To Delete Email
        [HttpDelete]
        public IHttpActionResult Delete(Int32 id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }


                return Ok(fileUploadService.DeleteFile(id));
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("File does not exits"))
                {
                    return NotFound();
                }
                return InternalServerError(new Exception(ex.Message));
            }
        }

        [HttpGet]
        public HttpResponseMessage GetFile(Int32 Id)
        {

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);


           var attch = mailAttachmentService.GetAttachment(Id);

            var fileLocation  = Path.Combine(filePath, attch.MailId.ToString());

           var fileFameToDownload = Path.Combine(fileLocation, attch.Attachment);


            if (!File.Exists(fileFameToDownload))
            {
                response.StatusCode = HttpStatusCode.NotFound;
                throw new HttpResponseException(response);
            }

            byte[] bytes = File.ReadAllBytes(fileFameToDownload);

            response.Content = new ByteArrayContent(bytes);
            response.Content.Headers.ContentLength = bytes.Length;
            response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = attch.Attachment;
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(MimeMapping.GetMimeMapping(attch.Attachment));

            return response;

        }
    }
}
