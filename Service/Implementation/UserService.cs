using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.DAL.Interface;
using MyBookmarksAPI.DAL.Wrapper;
using MyBookmarksAPI.Domain.Helpers;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Domain.TDOModel;
using MyBookmarksAPI.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<User> Create(UserCreateTDO model)
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
                Folder tmpFolder = await _repositoryWrapper.Folder.Create(new Folder
                {
                    Name = $"Папка №{i + 1}",
                    UserId = userId,
                });
                arrFolder.Add(tmpFolder);
            }

            return arrFolder;
        }

        public async Task Delete(long id) => _repositoryWrapper.User.Delete(await GetyById(id));

        public async Task<bool> EntityExists(long id) => await _repositoryWrapper.User.UserExists(id);

        public async Task<bool> EntityExists(string email) => await _repositoryWrapper.User.UserExists(email);

        public async Task<List<User>> GetAll() => await _repositoryWrapper.User.GetAll().ToListAsync();

        public async Task<User> GetyById(long id) => await _repositoryWrapper.User.GetByCondition(u => u.Id == id);

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void Save() => _repositoryWrapper.SaveAsync();
    }
}
