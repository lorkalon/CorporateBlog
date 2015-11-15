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

            Mapper.CreateMap<DAL.Models.User, ApplicationUser>();
            Mapper.CreateMap<ApplicationUser, DAL.Models.User>();

            Mapper.CreateMap<UserModel, ApplicationUser>();
            Mapper.CreateMap<ApplicationUser, UserModel>();



        }
    }
}