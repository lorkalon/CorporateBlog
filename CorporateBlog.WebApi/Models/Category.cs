using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorporateBlog.WebApi.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

        public virtual UserModel User { get; set; }
    }
}