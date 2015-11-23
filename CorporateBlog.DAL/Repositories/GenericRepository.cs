using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Models;
using CorporateBlog.DAL.Repositories.Filters;

namespace CorporateBlog.DAL.Repositories
{
    public class GenericRepository<TDataEntity> : IGenericRepository<TDataEntity>
        where TDataEntity : BaseEntity
    {
        private readonly DbSet<TDataEntity> _dbSet;
        private readonly DbContext _context;

        public GenericRepository(IContextCreator contextCreator)
        {
            _context = contextCreator.GetContext;
            _dbSet = contextCreator.GetContext.Set<TDataEntity>();
        }


        public void Add(TDataEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "Entity cann't be null!");
            }

            entity.CreatedOnUtc = DateTime.UtcNow;
            _dbSet.Add(entity);
        }

        public void Update(TDataEntity entity)
        {
            _dbSet.Attach(entity);

            var entry = _context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void Delete(TDataEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "Entity cann't be null!");
            }

            _dbSet.Remove(entity);
        }


        protected virtual IQueryable<TDataEntity> GetPaged(
            int from = 0, 
            int count=0, 
            Expression<Func<TDataEntity, bool>> where = null,
            Func<IQueryable<TDataEntity>, IOrderedQueryable<TDataEntity>> orderBy = null)
        {
            return orderBy(_dbSet.Where(where)).Skip(from).Take(count);
        }

        public async Task<IEnumerable<TDataEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }


        public async Task<TDataEntity> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

    }
}
