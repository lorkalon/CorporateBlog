using Autofac;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Repositories;

namespace CorporateBlog.DAL
{
    public static class DependeciesResolver
    {
        public static void Resolve(ContainerBuilder builder)
        {
            builder.RegisterType<CorporateBlogContextCreator>().As<IContextCreator>().InstancePerRequest();
            builder.RegisterType<CorporateBlogContextProvider>().As<IContextProvider>();

            builder.RegisterType<ArticleRateRepository>().As<IArticleRateRepository>();
            builder.RegisterType<ArticleRepository>().As<IArticleRepository>();
            builder.RegisterType<CategoryRepository>().As<ICategoryRepository>();
            builder.RegisterType<CommentRateRepository>().As<ICommentRateRepository>();
            builder.RegisterType<CommentRepository>().As<ICommentRepository>();
            builder.RegisterType<RoleRepository>().As<IRoleRepository>();
            builder.RegisterType<UserInfoRepository>().As<IUserInfoRepository>();
            builder.RegisterType<UserRepository>().As<IUserRepository>();

        }
    }
}
