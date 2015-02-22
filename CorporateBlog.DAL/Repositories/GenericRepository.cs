using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CorporateBlog.DAL.IRepositories;

namespace CorporateBlog.DAL.Repositories
{
    public class GenericRepository<TEntity, TDataEntity> : IGenericRepository<TEntity, TDataEntity>
        where TEntity : class
        where TDataEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TDataEntity> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TDataEntity>();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "Entity cann't be null!");
            }

            var dataEntity = Mapper.Map<TDataEntity>(entity);
            _dbSet.Add(dataEntity);
        }

        public void Update(TEntity entity)
        {
            var dataEntity = Mapper.Map<TDataEntity>(entity);
            _dbSet.Attach(dataEntity);

            var entry = _context.Entry(dataEntity);
            entry.State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "Entity cann't be null!");
            }

            var dataEntity = Mapper.Map<TDataEntity>(entity);
            _dbSet.Remove(dataEntity);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsQueryable().Project().To<TEntity>();
        }

        public IEnumerable<TEntity> GetAllPaged(int page, int pageSize)
        {
            return _dbSet.AsEnumerable().Skip(pageSize).Take(page).AsQueryable().Project().To<TEntity>();
        }

        public TEntity Get(int id)
        {
            return Mapper.Map<TEntity>(_dbSet.Find(id));
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
