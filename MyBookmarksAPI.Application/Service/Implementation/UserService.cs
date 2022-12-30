using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.DAL.Wrapper;
using MyBookmarksAPI.Domain.DtoModel.UserDtoModel;
using MyBookmarksAPI.Domain.Helpers;
using MyBookmarksAPI.Domain.Helpers.ApiException.UserException;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Service.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Service
{
    public class UserService : IUserService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public UserService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<User> Create(UserCreateDto model)
        {
            var TmpSalt = HashPasswordSHA512.CreateSalt();
            User user = new User
            {
                Email = model.Email,
                Name = model.Name,
                Salt = TmpSalt,
                Password = HashPasswordSHA512.HashPasswordSalt(model.Password, TmpSalt),
            };
            await _repositoryWrapper.User.Create(user);            

            return user;
        }

        public async Task<List<Folder>> CreateStartFolders(int quantityFolder, long userId)
        {
            List<Folder> arrFolder = new(quantityFolder);

            for (int i = 0; i < quantityFolder; i++)
            {
                Folder folder = new Folder
                {
                    Name = $"Папка №{i + 1}",
                    UserId = userId,
                };

                await _repositoryWrapper.Folder.Create(folder);
                arrFolder.Add(folder);
            }

            return arrFolder;
        }

        public void Update(User model) => _repositoryWrapper.User.Update(model);

        public async Task Delete(long id) => _repositoryWrapper.User.Delete(await GetyById(id));

        public async Task<bool> EntityExists(long id) => await _repositoryWrapper.User.EntityExists(u => u.Id == id);

        public async Task<bool> EntityExists(string email) => await _repositoryWrapper.User.EntityExists(u => u.Email.Equals(email));

        public async Task<List<User>> GetAll() => await _repositoryWrapper.User.GetAll().ToListAsync();

        public async Task<User> GetyById(long id) => await _repositoryWrapper.User.GetByCondition(u => u.Id == id);

        public async Task Save() => await _repositoryWrapper.SaveAsync();

        public async Task<User> GetAllDataById(long id) => await _repositoryWrapper.User.GetAllDataUser(u => u.Id == id);

        public async Task ChangePassword(UserChangePassword model)
        {
            User user = await GetyById(model.Id);
            if(!HashPasswordSHA512.VerifyHash(model.CurrentPassword, user.Salt, user.Password))
            {
                throw new VerifyPasswordUserException("Не вірний поточний пароль");
            }
            user.Password = HashPasswordSHA512.HashPasswordSalt(model.Password, user.Salt);       
            _repositoryWrapper.User.Update(user);
        }

        public async Task<User> LoginUser(string email, string password)
        {
            User user = await _repositoryWrapper.User.GetByCondition(u => u.Email.Equals(email));

            if(!HashPasswordSHA512.VerifyHash(password, user.Salt, user.Password))
            {
                throw new VerifyPasswordUserException("Не вірний пароль");
            }

            return await GetAllDataById(user.Id);
        }
    }
}
