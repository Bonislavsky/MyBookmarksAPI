using MyBookmarksAPI.Domain.DtoModel.UserDtoModel;
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
        void Update(UserUpdateDto model);
        Task<User> Create(UserCreateDto model);
        Task<List<Folder>> CreateStartFolders(int quantityFolder, long userId);
    }
}
