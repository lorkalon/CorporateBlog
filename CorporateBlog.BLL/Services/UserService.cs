using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.Common;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;

namespace CorporateBlog.BLL.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(
            IContextProvider contextProvider,
            IUserRepository userRepository) : base(contextProvider)
        {
            _userRepository = userRepository;
        }

        public Common.UsersReport GetUsersReport(Common.Filters.UsersFilter filter)
        {
            Expression<Func<DAL.Models.User, bool>> where = null;

            if (filter.SearchContent != null)
            {
                var content = filter.SearchContent.ToLower();
                where = user =>  user.UserName.ToLower().Contains(content) ||
                                 user.Email.ToLower().Contains(content) ||
                                 (user.UserInfo != null && user.UserInfo.Name.ToLower().Contains(content)) ||
                                 (user.UserInfo != null && user.UserInfo.Surname.ToLower().Contains(content));
            }

            var filtered = _userRepository.GetFiltered(
                where,
                null,
                filter.From,
                filter.Count,
                filter.IsAscending,
                user => user.CreatedOnUtc)
                .ToList();

            var totalCount = _userRepository.GetTotalCount();

            return new UsersReport()
            {
                Users = filtered.Select(Mapper.Map<Common.User>),
                TotalCount = totalCount
            };
        }

        public async Task UpdateUser(GeneralUserInfo info)
        {
            var saved = await _userRepository.FindUserAsync(info.UserId);

            if (saved != null)
            {
                if (info.Blocked.HasValue)
                {
                    saved.Blocked = info.Blocked.Value;
                }

                if (info.RoleId.HasValue)
                {
                    saved.RoleId = info.RoleId.Value;
                }

                if (info.EmailConfirmed.HasValue)
                {
                    saved.EmailConfirmed = info.EmailConfirmed.Value;
                }

                _userRepository.Update(saved);
                await SaveChangesAsync();
            }
        }

        public async Task DeleteUser(int userId)
        {
            var saved = await _userRepository.FindUserAsync(userId);

            if (saved != null)
            {
                _userRepository.Delete(saved);
                await SaveChangesAsync();
            }
        }
    }
}
