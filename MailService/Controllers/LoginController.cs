using MailService.Models;
using MailService.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MailService.Controllers
{
    public class LoginController : ApiController
    {
        UserAccountsService uaService = new UserAccountsService();

        [HttpPost()]
        public IHttpActionResult Authenticate([FromBody]dtoLoginRequest loginRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                dtoUserAccount userAccount = uaService.Authenticate(loginRequest.EmailId, loginRequest.Password);

                if (userAccount == null)
                    return Unauthorized();
                else
                    return Ok(userAccount);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception(ex.Message));
            }

        }
    }
}
