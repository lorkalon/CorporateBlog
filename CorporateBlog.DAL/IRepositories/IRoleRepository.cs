﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.IRepositories
{
    public interface IRoleRepository : IGenericRepository<Common.Role, Role>
    {
    }
}
