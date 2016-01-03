using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CorporateBlog.Common;

namespace CorporateBlog.WebApi.Models
{
    public class ArticleRate
    {
        public int ArticleId { get; set; }
        public int UserId { get; set; }
        public RateType Value { get; set; }
    }
}