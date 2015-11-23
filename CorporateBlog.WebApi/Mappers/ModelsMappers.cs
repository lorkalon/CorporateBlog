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
            Mapper.CreateMap<Common.Role, WebApi.Models.Role>();
            Mapper.CreateMap<WebApi.Models.Role, Common.Role>();

            Mapper.CreateMap<DAL.Models.User, ApplicationUser>();
            Mapper.CreateMap<ApplicationUser, DAL.Models.User>();

            Mapper.CreateMap<UserModel, ApplicationUser>();
            Mapper.CreateMap<ApplicationUser, UserModel>();

            Mapper.CreateMap<DAL.Models.User, Common.User>();
            Mapper.CreateMap<Common.User, WebApi.Models.UserModel>();

            Mapper.CreateMap<DAL.Models.Article, Common.Article>();
            Mapper.CreateMap<Common.Article, DAL.Models.Article>();
            Mapper.CreateMap<Common.Article, WebApi.Models.Article>();
            Mapper.CreateMap<WebApi.Models.Article, Common.Article>();


            Mapper.CreateMap<DAL.Models.Category, Common.Category>();
            Mapper.CreateMap<Common.Category, DAL.Models.Category>();
            Mapper.CreateMap<Common.Category, WebApi.Models.Category>();
            Mapper.CreateMap<WebApi.Models.Category, Common.Category>();

            Mapper.CreateMap<Common.Filters.BaseFilter, WebApi.Models.Filters.BaseFilter>();
            Mapper.CreateMap<WebApi.Models.Filters.BaseFilter, Common.Filters.BaseFilter>();

        }
    }
}