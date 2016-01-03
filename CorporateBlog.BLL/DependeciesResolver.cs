using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CorporateBlog.BLL.IServices;
using CorporateBlog.BLL.Services;

namespace CorporateBlog.BLL
{
    public static class DependeciesResolver
    {
        public static void Resolve(ContainerBuilder builder)
        {
            builder.RegisterType<ArticleService>().As<IArticleService>();
            builder.RegisterType<ArticleRateService>().As<IArticleRateService>();
            builder.RegisterType<RoleService>().As<IRoleService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
        }
    }
}
