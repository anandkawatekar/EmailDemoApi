using AutoMapper;
using MailService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailService
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize((config) =>
            {

                config.CreateMap<UserAccount, dtoUserAccount>().ReverseMap();

                config.CreateMap<MailAttachment, dtoMailAttachment>().ReverseMap();
               
                config.CreateMap<dtoMailMessage,Mail>();
                config.CreateMap<Mail, dtoMailMessage>()
                .ForMember(c => c.FromUser, opt => opt.Ignore())
                .ForMember(c => c.ToUsersList, opt => opt.Ignore())
                .ForMember(c => c.AttachmentsList, opt => opt.Ignore());


            });
        }
    }
}