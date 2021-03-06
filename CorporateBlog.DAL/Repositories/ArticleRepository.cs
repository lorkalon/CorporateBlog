﻿using System;
using System.Collections.Generic;
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

        public IEnumerable<DateTime> GetDateLimit(int categoryId)
        {
            var categoryArticles = DbSet.Where(article => article.CategoryId == categoryId);

            var firstArticle = categoryArticles.OrderBy(article => article.CreatedOnUtc).FirstOrDefault();
            var lastArticle = categoryArticles.OrderByDescending(article => article.CreatedOnUtc).FirstOrDefault();

            if (firstArticle == null || lastArticle == null)
            {
                return null;
            }

            return new List<DateTime>()
            {
                firstArticle.CreatedOnUtc,
                lastArticle.CreatedOnUtc
            };
        }
    }
}
