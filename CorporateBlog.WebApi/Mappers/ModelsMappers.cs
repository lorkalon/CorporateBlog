using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using AutoMapper;
using CorporateBlog.WebApi.Authentication;
using CorporateBlog.WebApi.Models;

namespace CorporateBlog.WebApi.Mappers
{
    public static class ModelsMappers
    {
        public static void RegisterMappers()
        {
            Mapper.CreateMap<DAL.Models.Role, Common.Role>();
            Mapper.CreateMap<Common.Role, DAL.Models.Role>();

            Mapper.CreateMap<DAL.Models.User, Common.User>();
            Mapper.CreateMap<Common.User, DAL.Models.User>();

            Mapper.CreateMap<ApplicationUser, Common.User>()
                .ForMember(t => t.Id, m => m.MapFrom(s => s.Id))
                .ForMember(t => t.Login, m => m.MapFrom(s => s.UserName))
                .ForMember(t => t.Email, m => m.MapFrom(s => s.Email))
                .ForMember(t => t.Confirmed, m => m.MapFrom(s => s.Confirmed))
                .ForMember(t => t.Blocked, m => m.MapFrom(s => s.Blocked))
                .ForMember(t => t.Password, m => m.MapFrom(s => s.PasswordHash));

            Mapper.CreateMap<Common.User, ApplicationUser>()
               .ForMember(t => t.Id, m => m.MapFrom(s => s.Id))
               .ForMember(t => t.UserName, m => m.MapFrom(s => s.Login))
               .ForMember(t => t.Email, m => m.MapFrom(s => s.Email))
               .ForMember(t => t.Confirmed, m => m.MapFrom(s => s.Confirmed))
               .ForMember(t => t.Blocked, m => m.MapFrom(s => s.Blocked))
               .ForMember(t => t.PasswordHash, m => m.MapFrom(s => s.Password));

            Mapper.CreateMap<UserModel, ApplicationUser>()
                .ForMember(t => t.UserName, m => m.MapFrom(s => s.Login))
                .ForMember(t => t.PasswordHash, m => m.MapFrom(s => s.Password))
                .ForMember(t => t.Email, m => m.MapFrom(s => s.Email))
                .ForMember(t => t.Confirmed, m => m.MapFrom(s => s.Confirmed))
                .ForMember(t => t.Blocked, m => m.MapFrom(s => s.Blocked))
                .ForMember(t => t.Id, m => m.MapFrom(s => s.Id));


        }
    }
}