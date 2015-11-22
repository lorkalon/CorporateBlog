using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateBlog.BLL.IServices
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(Common.Category category);
        Task DeleteCategoryAsync(int categoryId);
        Task<IEnumerable<Common.Category>> GetAllAsync();
    }
}
