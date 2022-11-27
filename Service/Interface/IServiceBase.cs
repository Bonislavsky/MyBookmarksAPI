using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Service.Interface
{
    public interface IServiceBase<T>
    {
        Task<bool> EntityExists(long id);
        Task<T> GetyById(long id);
        Task Delete(long id);
        Task Save();
        void Update(T model);
    }
}
