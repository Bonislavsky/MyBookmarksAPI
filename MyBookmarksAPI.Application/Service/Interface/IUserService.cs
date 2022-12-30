using MyBookmarksAPI.Domain.DtoModel.UserDtoModel;
using MyBookmarksAPI.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Service.Interface
{
    public interface IUserService : IServiceBase<User>
    {
        Task<List<User>> GetAll();
        Task<bool> EntityExists(string email);
        Task<User> Create(UserCreateDto model);
        Task<List<Folder>> CreateStartFolders(int quantityFolder, long userId);
        Task<User> GetAllDataById(long id);
        Task<User>LoginUser(string email, string password);
        Task ChangePassword(UserChangePassword model);
    }
}
