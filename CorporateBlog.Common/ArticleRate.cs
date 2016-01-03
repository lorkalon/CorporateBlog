using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.Common
{
    public class ArticleRate
    {
        public int Id { get; set; }
        public RateType Value { get; set; }

        public int UserId { get; set; }
        public Common.User User { get; set; }

        public int ArticleId { get; set; }
        public Common.Article Article { get; set; }

    }
}
