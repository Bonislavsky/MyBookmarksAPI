using MyBookmarksAPI.DAL.Interface;
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
        private readonly IUserRepository _userRepository;
        private readonly IFolderRepository _folderRepository;

        public UserService(IUserRepository userRepository, IFolderRepository folderRepository)
        {
            _userRepository = userRepository;
            _folderRepository = folderRepository;
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
            await _userRepository.Create(user);
            return user;
        }

        public async Task<List<Folder>> CreateStartFolders(int quantityFolder, long userId)
        {
            List<Folder> arrFolder = new(quantityFolder);

            for (int i = 0; i < quantityFolder; i++)
            {
                Folder tmpFolder = await _folderRepository.Create(new Folder
                {
                    Name = $"Папка №{i + 1}",
                    UserId = userId,
                });
                arrFolder.Add(tmpFolder);
            }

            return arrFolder;
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetyById(long id)
        {
            return await _userRepository.GetByCondition(u => u.Id == id);
        }

        public void Save()
        {
            _userRepository.Save();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
