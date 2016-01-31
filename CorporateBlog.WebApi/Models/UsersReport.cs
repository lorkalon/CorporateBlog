using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorporateBlog.WebApi.Models
{
    public class UsersReport
    {
        public IEnumerable<UserModel> Users { get; set; }
        public int TotalCount { get; set; } 
    }
}