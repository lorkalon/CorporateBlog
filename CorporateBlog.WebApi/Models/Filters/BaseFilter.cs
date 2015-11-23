namespace CorporateBlog.WebApi.Models.Filters
{
    public class BaseFilter
    {
        public int From { get; set; }
        public int Count { get; set; }
        public bool IsAscending { get; set; }
        public string SearchContent { get; set; }
        public string OrderByField { get; set; }
    }
}