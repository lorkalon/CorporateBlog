﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CorporateBlog.WebApi.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool Blocked { get; set; }
    }
}