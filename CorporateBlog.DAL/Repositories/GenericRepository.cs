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

        public IEnumerable<TDataEntity> GetFiltered(
            Expression<Func<TDataEntity, bool>> whereExpression = null,
            Expression<Func<TDataEntity, object>> orderByExpression = null,
            int?from = null, int? count = null, bool? isAsc = null)
        {
            var articles = DbSet.Select(x => x);

            if (whereExpression != null)
            {
                articles = articles.Where(whereExpression);
            }

            if (orderByExpression!=null)
            {
                if (!isAsc.HasValue)
                {
                    articles.OrderBy(orderByExpression);
                }
                else
                {
                    if (isAsc.Value)
                    {
                        articles.OrderBy(orderByExpression);
                    }
                    else
                    {
                        articles.OrderByDescending(orderByExpression);
                    }
                }
            }

            if (from.HasValue)
            {
                articles = articles.Skip(from.Value);
            }

            if (count.HasValue)
            {
                articles = articles.Take(count.Value);
            }

            var result = articles;

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
