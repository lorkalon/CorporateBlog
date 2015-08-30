using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Repositories;

namespace CorporateBlog.DAL
{
    public class DependeciesResolver
    {
        private readonly ContainerBuilder _containerBuilder;

        public DependeciesResolver(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }

        public void RegisterTypes()
        {
            _containerBuilder.RegisterType<CorporateBlogContextCreator>().As<IContextCreator>().InstancePerRequest();
            _containerBuilder.RegisterType<CorporateBlogContextProvider>().As<IContextProvider>();

            _containerBuilder.RegisterType<ArticleRateRepository>().As<IArticleRateRepository>();
            _containerBuilder.RegisterType<ArticleRepository>().As<IArticleRepository>();
            _containerBuilder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            _containerBuilder.RegisterType<CommentRateRepository>().As<ICommentRateRepository>();
            _containerBuilder.RegisterType<CommentRepository>().As<ICommentRepository>();
            _containerBuilder.RegisterType<RoleRepository>().As<IRoleRepository>();
            _containerBuilder.RegisterType<UserInfoRepository>().As<IUserInfoRepository>();
            _containerBuilder.RegisterType<UserRepository>().As<IUserRepository>();

        }
    }
}
