using MyBookmarksAPI.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Service.Interface
{
    public interface IFolderService : IServiceBase<Folder>
    {
        Task<Folder> Create(Folder model);
        Task<Folder> GetAllDataById(long id);
        Task<List<Folder>> GetListByUserId(long userId, string sortParam, string AsdDec);
        Task<bool> UserByIdExists(long id);
    }
}
