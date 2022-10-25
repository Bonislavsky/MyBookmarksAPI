using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.DAL.Interface
{
    interface IRepositoryBase<T>
    {
        IQueryable<T> GetAll();
        Task<T> GetById(long id);
        Task<T> Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
