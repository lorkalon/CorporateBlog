using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.Common.Filters
{
    public class ArticleFilter:BaseFilter
    {
        public int? CategoryId { get; set; }
        public int? CreatedById { get; set; }

        public DateTime? MinCreationDate { get; set; }
        public DateTime? MaxCreationDate { get; set; }

    }
}
