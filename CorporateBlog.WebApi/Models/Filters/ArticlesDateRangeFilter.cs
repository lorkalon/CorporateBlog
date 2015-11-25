using System;

namespace CorporateBlog.WebApi.Models.Filters
{
    public class ArticlesDateRangeFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int CategoryId { get; set; }
    }
}
