using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.Common;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;

namespace CorporateBlog.BLL.Services
{
    public class UserInfoService : BaseService, IUserInfoService
    {
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly IUserRepository _userRepository;
        public UserInfoService(
            IContextProvider contextProvider, 
            IUserInfoRepository userInfoRepository, 
            IUserRepository userRepository): base(contextProvider)
        {
            _userInfoRepository = userInfoRepository;
            _userRepository = userRepository;
        }

        public async Task AddOrUpdateUserInfo(Common.UserInfo userInfo)
        {
            var savedInfo = _userInfoRepository.GetFiltered(info => info.UserId == userInfo.UserId).FirstOrDefault();

            if (savedInfo == null)
            {
                var entity = Mapper.Map<DAL.Models.UserInfo>(userInfo);
                _userInfoRepository.Add(entity);
                await SaveChangesAsync();
            }
            else
            {
                savedInfo.Avatar = userInfo.Avatar;
                savedInfo.Name = userInfo.Name;
                savedInfo.Surname = userInfo.Surname;
                _userInfoRepository.Update(savedInfo);
                await SaveChangesAsync();
            }

        }

        public Common.UserInfo FindUserInfoByUserId(int userId)
        {
            var entity = _userInfoRepository.GetFiltered(info => info.UserId == userId).FirstOrDefault();

            if (entity == null)
            {
                return null;
            }

            var mapped = Mapper.Map<Common.UserInfo>(entity);
            return mapped;
        }
    }
}
