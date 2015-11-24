using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Models;

namespace CorporateBlog.DAL.Repositories
{
    public class GenericRepository<TDataEntity> : IGenericRepository<TDataEntity>
        where TDataEntity : BaseEntity
    {
        protected readonly DbSet<TDataEntity> DbSet;
        private readonly DbContext _context;

        public GenericRepository(IContextCreator contextCreator)
        {
            _context = contextCreator.GetContext;
            DbSet = contextCreator.GetContext.Set<TDataEntity>();
        }


        public void Add(TDataEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "Entity cann't be null!");
            }

            entity.CreatedOnUtc = DateTime.UtcNow;
            DbSet.Add(entity);
        }

        public void Update(TDataEntity entity)
        {
            DbSet.Attach(entity);

            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(TDataEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "Entity cann't be null!");
            }

            DbSet.Remove(entity);
        }

        public IEnumerable<TDataEntity> GetPaged(
            List<Expression<Func<TDataEntity, bool>>> whereExpressions = null,
            Expression<Func<TDataEntity, object>> orderByExpression = null,
            Models.Filters.BaseFilter filter = null)
        {
            var articles = DbSet.Select(x => x);

            if (whereExpressions != null)
            {
                articles = whereExpressions.Aggregate(articles, (current, @where) => current.Where(@where));
            }

            if (orderByExpression!=null)
            {
                if (filter == null)
                {
                    articles.OrderBy(orderByExpression);
                }
                else
                {
                    if (filter.IsAscending)
                    {
                        articles.OrderBy(orderByExpression);
                    }
                    else
                    {
                        articles.OrderByDescending(orderByExpression);
                    }
                }
            }

            if (filter != null)
            {
                articles = articles.Skip(filter.From).Take(filter.Count);
            }

            var result = articles.ToList();

            return result;
        }

        public async Task<IEnumerable<TDataEntity>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }


        public async Task<TDataEntity> GetAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

    }
}
