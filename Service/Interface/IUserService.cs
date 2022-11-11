using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Domain.TDOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Service.Interface
{
    public interface IUserService : IServiceBase<User>
    {
        void Update(UserDto model);
        Task<User> Create(UserDto model);
        Task<List<Folder>> CreateStartFolders(int quantityFolder, long userId);
    }
}
