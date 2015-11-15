using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.Repositories.Filters;

namespace CorporateBlog.DAL.IRepositories
{
    public interface IGenericRepository<TDataEntity>
        where TDataEntity : class
    {
        void Add(TDataEntity entity);

        void Update(TDataEntity entity);

        void Delete(TDataEntity entity);

        IEnumerable<TDataEntity> GetPaged(BaseFilter<TDataEntity> filter = null);

        Task<IEnumerable<TDataEntity>> GetAllAsync();

        Task<TDataEntity> GetAsync(int id);
    }
}
