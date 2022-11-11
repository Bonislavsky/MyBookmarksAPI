using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Service.Interface
{
    public interface IServiceBase<T>
    {
        Task<List<T>> GetAll();
        Task<T> GetyById(long id);
        Task<bool> EntityExists(long id);
        Task<bool> EntityExists(string email);
        void Update(T entity);
        Task Delete(long id);
        void Save();
    }
}
