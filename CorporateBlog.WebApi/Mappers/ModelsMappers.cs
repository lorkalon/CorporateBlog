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

            Mapper.CreateMap<DAL.Models.Article, Common.Article>()
                .AfterMap((src, dest) => dest.Rate = src.ArticleRates.Sum(rate => rate.Value));
            Mapper.CreateMap<Common.Article, DAL.Models.Article>();
            Mapper.CreateMap<Common.Article, WebApi.Models.Article>();
            Mapper.CreateMap<WebApi.Models.Article, Common.Article>();


            Mapper.CreateMap<DAL.Models.Category, Common.Category>();
            Mapper.CreateMap<Common.Category, DAL.Models.Category>();
            Mapper.CreateMap<Common.Category, WebApi.Models.Category>();
            Mapper.CreateMap<WebApi.Models.Category, Common.Category>();

            Mapper.CreateMap<Common.Filters.ArticlesDateRangeFilter, WebApi.Models.Filters.ArticlesDateRangeFilter>();
            Mapper.CreateMap<WebApi.Models.Filters.ArticlesDateRangeFilter, Common.Filters.ArticlesDateRangeFilter>();

            Mapper.CreateMap<DAL.Models.Comment, Common.Comment>()
               .AfterMap((src, dest) => dest.Rate = src.CommentRates.Sum(rate => rate.Value));
            Mapper.CreateMap<Common.Comment, DAL.Models.Comment>();
            Mapper.CreateMap<Common.Comment, WebApi.Models.Comment>();
            Mapper.CreateMap<WebApi.Models.Comment, Common.Comment>();

        }
    }
}