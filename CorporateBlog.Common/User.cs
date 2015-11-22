using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.Common
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public virtual Common.UserInfo UserInfo { get; set; }
        public int RoleId { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool Blocked { get; set; }
        public virtual Common.Role Role { get; set; }

    }
}
