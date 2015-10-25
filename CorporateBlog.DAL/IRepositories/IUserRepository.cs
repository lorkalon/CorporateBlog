﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.IRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        DAL.Models.User FindUser(string login);
        DAL.Models.User FindUser(int userId);
        DAL.Models.User FindUserByEmail(string email);
    }
}
