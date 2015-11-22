using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using CorporateBlog.BLL.IServices;
using CorporateBlog.DAL.Models;
using CorporateBlog.WebApi.Authentication;
using CorporateBlog.WebApi.Models;
using Microsoft.AspNet.Identity;

namespace CorporateBlog.WebApi.Controllers
{
    [Authorize]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly ApplicationUserManager _userManager;
        public CategoryController(ICategoryService categoryService, ApplicationUserManager userManager)
        {
            _categoryService = categoryService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("api/Category/Create")]
        public async Task CreateCategory(Models.Category category)
        {
            var model = Mapper.Map<Common.Category>(category);
            var userName = User.Identity.GetUserName();
            var user = await _userManager.FindByNameAsync(userName);
            model.UserId = user.Id;

            await _categoryService.CreateCategoryAsync(model);
        }



        [HttpGet]
        [Route("api/Category/GetAll")]
        public async Task<IEnumerable<Models.Category>> GetAllCategories()
        {
            var categories = await _categoryService.GetAllAsync();
            var mappedCategories = categories.Select(Mapper.Map<Models.Category>).ToList();
            return mappedCategories;
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("api/Category/Delete/{categoryId}")]
        public async Task DeleteCategory(int categoryId)
        {
            await _categoryService.DeleteCategoryAsync(categoryId);
        }
    }
}
