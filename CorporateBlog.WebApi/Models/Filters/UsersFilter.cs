using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorporateBlog.WebApi.Models.Filters
{
    public class UsersFilter
    {
        public int? From { get; set; }
        public int? Count { get; set; }
        public string SearchContent { get; set; }
        public bool? IsAscending { get; set; }
    }
}