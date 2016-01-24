using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorporateBlog.WebApi.Models
{
    public class UserModel
    {
        public UserModel()
        {
            EmailConfirmed = false;
            Blocked = false;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool Blocked { get; set; }

        public virtual WebApi.Models.Role Role { get; set; }
        public virtual WebApi.Models.UserInfo UserInfo { get; set; }
    }
}