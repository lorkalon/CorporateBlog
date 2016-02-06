using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.WebApi.Authentication;
using CorporateBlog.WebApi.Models;

namespace CorporateBlog.WebApi.Controllers
{
    [ExtendedAuthorize(Roles = RoleNames.Admin)]
    [RoutePrefix("api/Admin")]
    public class AdminController:BaseController
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AdminController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet]
        [Route("GetUsersReport")]
        public Models.UsersReport GetUsersReport([FromUri]Models.Filters.UsersFilter filter)
        {
            var commonFilter = Mapper.Map<Common.Filters.UsersFilter>(filter);
            var report = _userService.GetUsersReport(commonFilter);
            var mappedReport = Mapper.Map<Models.UsersReport>(report);
            return mappedReport;
        }

        [HttpPut]
        [Route("UpdateUser")]
        public async Task GetUsersReport(GeneralUserInfo generalInfo)
        {
            var info = Mapper.Map<Common.GeneralUserInfo>(generalInfo);
            await _userService.UpdateUser(info);
        }

        [HttpDelete]
        [Route("DeleteUser/{userId}")]
        public async Task DeleteUser(int userId)
        {
            await _userService.DeleteUser(userId);
        }

        [HttpGet]
        [Route("GetRoles")]
        public async Task<IEnumerable<Models.Role>> GetRoles()
        {
            var roles = await _roleService.GetRoles();
            return roles.Select(Mapper.Map<Models.Role>);
        } 
    }
}