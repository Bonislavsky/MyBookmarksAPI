using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyBookmarksAPI.DAL.Interface
{
    public interface IRepositoryBase<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByCondition(Expression<Func<T, bool>> expression);
        Task<T> Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
