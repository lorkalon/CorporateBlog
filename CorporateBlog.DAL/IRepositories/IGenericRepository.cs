using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CorporateBlog.DAL.DbContextProvider;
using CorporateBlog.DAL.Repositories.Filters;

namespace CorporateBlog.DAL.IRepositories
{
    public interface IGenericRepository<TDataEntity> 
        where TDataEntity:class
    {
        void Add(TDataEntity entity);
        
        void Update(TDataEntity entity);
        
        void Delete(TDataEntity entity);

        IEnumerable<TDataEntity> GetPaged();

        IEnumerable<TDataEntity> GetAll();
    }
}
