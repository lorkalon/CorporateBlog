using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.Common
{
    public class UsersReport
    {
        public IEnumerable<Common.User> Users { get; set; }
        public int TotalCount { get; set; }
    }
}
