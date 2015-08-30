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
    public class DependeciesResolver
    {
        private readonly ContainerBuilder _containerBuilder;

        public DependeciesResolver(ContainerBuilder containerBuilder)
        {
            _containerBuilder = containerBuilder;
        }

        public void RegisterTypes()
        {
            _containerBuilder.RegisterType<ArticleService>().As<IArticleService>();
        }
    }
}
