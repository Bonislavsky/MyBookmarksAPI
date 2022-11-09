using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Service.Interface
{
    public interface IServiceBase<T>
    {
        Task<IQueryable<T>> GetAll();
        Task<T> GetyById(long id);
        void Update(T entity);
        void Delete(long id);
        void Save();
    }
}
