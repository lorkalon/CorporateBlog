using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using CorporateBlog.Common.Filters;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.Repositories
{
    public class ArticleRepository : GenericRepository<Article>, IArticleRepository
    {
        public ArticleRepository(IContextCreator contextCreator)
            : base(contextCreator)
        {
        }

        public IEnumerable<Article> GetPaged(BaseFilter filter)
        {
            var searchContent = filter.SearchContent.ToLower();

            Expression<Func<Article, bool>> where = article => article.Title.ToLower().Contains(searchContent) ||
                                                               article.Text.ToLower().Contains(searchContent);

            Func<IQueryable<Article>, IOrderedQueryable<Article>> orderBy = articles => articles.OrderBy(article => article.Id);

            switch (filter.OrderByField)
            {
                case "Id":
                {
                    if (filter.IsAscending)
                    {
                        orderBy = articles => articles.OrderBy(article => article.Id);
                    }
                    else
                    {
                        orderBy = articles => articles.OrderByDescending(article => article.Id);
                    }

                    break;
                }
                case "Title":
                {
                    if (filter.IsAscending)
                    {
                        orderBy = articles => articles.OrderBy(article => article.Title);
                    }
                    else
                    {
                        orderBy = articles => articles.OrderByDescending(article => article.Title);
                    }

                    break;
                }
                     case "Date":
                {
                    if (filter.IsAscending)
                    {
                        orderBy = articles => articles.OrderBy(article => article.CreatedOnUtc);
                    }
                    else
                    {
                        orderBy = articles => articles.OrderByDescending(article => article.CreatedOnUtc);
                    }

                    break;
                }
                     
            } 

            return base.GetPaged(filter.From, filter.Count, where, orderBy).ToList();
        }
    }
}
