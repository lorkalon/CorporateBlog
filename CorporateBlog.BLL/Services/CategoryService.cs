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
    public class CategoryService : BaseService, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository, IContextProvider contextProvider) : base(contextProvider)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task CreateCategoryAsync(Common.Category category)
        {
            var entity = Mapper.Map<DAL.Models.Category>(category);
            _categoryRepository.Add(entity);
            await SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(int categoryId)
        {
            var category = await _categoryRepository.GetAsync(categoryId);
            _categoryRepository.Delete(category);
            await SaveChangesAsync();
        }

        public async Task<IEnumerable<Common.Category>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            var mappedCategories = categories.Select(Mapper.Map<Common.Category>).ToList();
            return mappedCategories;
        }
    }
}
