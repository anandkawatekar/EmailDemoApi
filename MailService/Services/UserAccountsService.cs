using AutoMapper;
using MailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailService.Services
{
    public class UserAccountsService:IDisposable
    {
        private MailManagerDBConnection dataContext;

        public UserAccountsService()
        {
            this.dataContext = new MailManagerDBConnection();
        }

        public dtoUserAccount Authenticate(string eMailId, string password)
        {
            try
            {
                var userAccount = dataContext.UserAccounts.SingleOrDefault(x => x.EmailId == eMailId && x.Password == password);
                dtoUserAccount dtoUser = Mapper.Map<dtoUserAccount>(userAccount);
                return dtoUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public List<dtoUserAccount> GetUserAccounts()
        {
            try
            {

                var userAccounts = dataContext.UserAccounts.ToList().Select(x => { x.Password = null; return x; }).ToList();
                List<dtoUserAccount> dtoUser = Mapper.Map<List<dtoUserAccount>>(userAccounts);
                return dtoUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dtoUserAccount GetUserAccount(Int32 UserId)
        {
            try
            {
                var userAccount = dataContext.UserAccounts.Find(UserId);
                dtoUserAccount dtoUser = Mapper.Map<dtoUserAccount>(userAccount);
                return dtoUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public dtoUserAccount GetUserAccountsByeMailId(string eMailId)
        {
            try
            {
                var userAccount = dataContext.UserAccounts.Where(x => x.EmailId == eMailId).FirstOrDefault();
                dtoUserAccount dtoUser = Mapper.Map<dtoUserAccount>(userAccount);
                return dtoUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Dispose()
        {
            ((IDisposable)dataContext).Dispose();
        }
    }
}