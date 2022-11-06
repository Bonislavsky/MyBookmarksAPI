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
        Task<User> Create(UserCreateTDO entity);
        void CreateStartFolders(int quantityFolder, long userId);
    }
}
