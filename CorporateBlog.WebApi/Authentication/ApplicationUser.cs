using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace CorporateBlog.WebApi.Authentication
{
    public class ApplicationUser:IUser<int>
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }
       
        public int RoleId { get; set; }

    }
}