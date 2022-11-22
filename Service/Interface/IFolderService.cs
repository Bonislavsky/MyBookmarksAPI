using MyBookmarksAPI.Domain.DtoModel;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Domain.TDOModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Service.Interface
{
    public interface IFolderService : IServiceBase<Folder>
    {
        Task<Folder> Create(Folder model);
        Task<List<Folder>> GetListByUserId(long userId);
        Task<bool> UserByIdExists(long id);
    }
}
