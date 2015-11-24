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
    }
}
