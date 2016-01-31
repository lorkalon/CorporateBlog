using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorporateBlog.WebApi.Models
{
    public class GeneralUserInfo
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool Blocked { get; set; }
    }
}