﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.Repositories.Filters
{
    public class BaseFilter<TDataEntity>
    {
        public int From { get; set; }
        public int Count { get; set; }
        public bool IsAscending { get; set; }
        public string SearchContent { get; set; }
        public Func<TDataEntity, bool> OrderBy { get; set; }
        public Func<TDataEntity, bool> SearchQuery { get; set; }
    }
}
