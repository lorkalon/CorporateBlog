using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.Common.Filters
{
    public class CommentsFilter
    {
        public int ArticleId { get; set; }
        public int From { get; set; }
        public int Count { get; set; }
    }
}
