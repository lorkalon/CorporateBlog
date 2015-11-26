using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.Common.Filters
{
    public class CommentsDateRangeFilter
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int ArticleId { get; set; }
    }
}
