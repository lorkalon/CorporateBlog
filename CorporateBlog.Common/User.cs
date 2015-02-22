﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.Common
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int Salt { get; set; }

        public int? UserInfoId { get; set; }
        public int RoleId { get; set; }
    }
}
