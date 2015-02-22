using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.Common
{
    public class CommentRate
    {
        public int Id { get; set; }
        public int Value { get; set; }

        public int UserId { get; set; }
        public int CommentId { get; set; }
    }
}
