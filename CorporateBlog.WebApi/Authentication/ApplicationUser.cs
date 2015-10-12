using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace CorporateBlog.WebApi.Authentication
{
    public class ApplicationUser:IUser<int>, IUser<string>
    {
        public int Id { get; set; }

        string IUser<string>.Id
        {
            get { return Id.ToString(); }
        }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }
       
        public int RoleId { get; set; }

    }
}