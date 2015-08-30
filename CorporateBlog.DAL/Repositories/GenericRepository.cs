using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.IRepositories;
using CorporateBlog.DAL.Repositories.Filters;

namespace CorporateBlog.DAL.Repositories
{
    public class GenericRepository<TDataEntity> : IGenericRepository<TDataEntity>
        where TDataEntity : class
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

        public IEnumerable<TDataEntity> GetPaged()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<TDataEntity> GetAll()
        {
            return _dbSet.ToList();
        }


        public TDataEntity Get(int id)
        {
            return _dbSet.Find(id);
        }

    }
}
