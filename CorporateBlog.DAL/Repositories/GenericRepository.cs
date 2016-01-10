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
            int? from = null, int? count = null, bool? isAsc = null,
            Expression<Func<TDataEntity, DateTime>> orderByDateTime = null)
        {
            var entities = DbSet.Select(x => x);
            Expression<Func<TDataEntity, DateTime>> orderBy = null;

            if (whereExpression != null)
            {
                entities = entities.Where(whereExpression);
            }

            if (orderByExpression != null)
            {
                entities = SortEntities(orderByExpression, entities, isAsc);
            }

            if (orderByDateTime != null)
            {
                entities = SortEntitiesWithDateTime(orderByDateTime, entities, isAsc);
            }

            if (from.HasValue)
            {
                entities = entities.Skip(from.Value);
            }

            if (count.HasValue)
            {
                entities = entities.Take(count.Value);
            }

            var result = entities;

            return result;
        }


        private IQueryable<TDataEntity> SortEntities(
            Expression<Func<TDataEntity, object>> orderByExpression, 
            IQueryable<TDataEntity> entities, 
            bool? isAsc = null)
        {
            if (!isAsc.HasValue)
            {
                entities = entities.OrderBy(orderByExpression);
            }
            else
            {
                if (isAsc.Value)
                {
                    entities = entities.OrderBy(orderByExpression);
                }
                else
                {
                    entities = entities.OrderByDescending(orderByExpression);
                }
            }

            return entities;
        }

        private IQueryable<TDataEntity> SortEntitiesWithDateTime(
            Expression<Func<TDataEntity, DateTime>> orderByExpression,
            IQueryable<TDataEntity> entities,
            bool? isAsc = null)
        {
            if (!isAsc.HasValue)
            {
                entities = entities.OrderBy(orderByExpression);
            }
            else
            {
                if (isAsc.Value)
                {
                    entities = entities.OrderBy(orderByExpression);
                }
                else
                {
                    entities = entities.OrderByDescending(orderByExpression);
                }
            }

            return entities;
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
