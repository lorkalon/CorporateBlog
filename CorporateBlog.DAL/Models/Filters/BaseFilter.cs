using System;

namespace CorporateBlog.DAL.Models.Filters
{
    public class BaseFilter
    {
        public int From { get; set; }
        public int Count { get; set; }
        public bool IsAscending { get; set; }
        public string SearchContent { get; set; }

    }
}
