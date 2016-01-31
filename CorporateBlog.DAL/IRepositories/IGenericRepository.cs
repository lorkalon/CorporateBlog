using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CorporateBlog.DAL.IRepositories
{
    public interface IGenericRepository<TDataEntity>
        where TDataEntity : class
    {
        void Add(TDataEntity entity);

        void Update(TDataEntity entity);

        void Delete(TDataEntity entity);

        Task<IEnumerable<TDataEntity>> GetAllAsync();

        Task<TDataEntity> GetAsync(int id);

        IEnumerable<TDataEntity> GetFiltered(
            Expression<Func<TDataEntity, bool>> whereExpressions = null,
            Expression<Func<TDataEntity, object>> orderByExpression = null,
            int? from = null, int? count = null, bool? isAsc = null,
            Expression<Func<TDataEntity, DateTime>> orderByDateTime = null);

        int GetTotalCount();

    }
}
