using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.DAL.Interface;
using MyBookmarksAPI.DAL.Wrapper;
using MyBookmarksAPI.Domain.DtoModel.UserDtoModel;
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
        private readonly IMapper _mapper;

        public UserService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<UserDto> Create(UserCreateDto model)
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

            return _mapper.Map<UserDto>(user);
        }

        public async Task<List<FolderDto>> CreateStartFolders(int quantityFolder, long userId)
        {
            List<FolderDto> arrFolder = new(quantityFolder);

            for (int i = 0; i < quantityFolder; i++)
            {
                Folder folder = new Folder
                {
                    Name = $"Папка №{i + 1}",
                    UserId = userId,
                };

                await _repositoryWrapper.Folder.Create(folder);
                arrFolder.Add(_mapper.Map<FolderDto>(folder));
            }

            return arrFolder;
        }

        public async Task Delete(long id) => _repositoryWrapper.User.Delete(await GetyById(id));

        public async Task<bool> EntityExists(long id) => await _repositoryWrapper.User.UserExists(id);

        public async Task<bool> EntityExists(string email) => await _repositoryWrapper.User.UserExists(email);

        public async Task<List<User>> GetAll() => await _repositoryWrapper.User.GetAll().ToListAsync();

        public async Task<User> GetyById(long id) => await _repositoryWrapper.User.GetByCondition(u => u.Id == id);

        public async Task<UserDto> Update(UserUpdateDto model)
        {
            User user = await GetyById(model.Id);

            _mapper.Map(model, user);

            return _mapper.Map<UserDto>(user); 
        }

        public async Task Save() => await _repositoryWrapper.SaveAsync();

    }
}
