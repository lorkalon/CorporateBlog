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

        IEnumerable<TDataEntity> GetPaged(
            List<Expression<Func<TDataEntity, bool>>> whereExpressions,
            Expression<Func<TDataEntity, object>> orderByExpression,
            Models.Filters.BaseFilter filter);
    }
}
