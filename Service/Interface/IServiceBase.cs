using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Service.Interface
{
    public interface IServiceBase<T>
    {
        Task<IQueryable<T>> GetEntities();
        Task<T> GetyById(long id);
        Task<T> Create(T entity);
        void Update(T entity);
        void Delete(long id);
    }
}
