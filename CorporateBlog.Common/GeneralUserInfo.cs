using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.Common
{
    public class GeneralUserInfo
    {
        public int UserId { get; set; }
        public int? RoleId { get; set; }
        public bool? EmailConfirmed { get; set; }
        public bool? Blocked { get; set; }
    }
}
