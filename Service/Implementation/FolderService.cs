using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.DAL.Wrapper;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Service.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Service.Implementation
{
    public class FolderService : IFolderService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public FolderService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task Delete(long id) => _repositoryWrapper.Folder.Delete(await GetyById(id));

        public async Task<bool> EntityExists(long id) => await _repositoryWrapper.Folder.EntityExists(id);

        public async Task<List<Folder>> GetListByUserId(long userId, string sortParam, string AsdDec)
        {
            return await _repositoryWrapper.Folder.GetListByCondition(f => f.UserId == userId).OrderBy($"{sortParam} {AsdDec}").ToListAsync();
        }

        public async Task<Folder> GetyById(long id) => await _repositoryWrapper.Folder.GetByCondition(u => u.Id == id);

        public async Task<bool> UserByIdExists(long userId) => await _repositoryWrapper.User.EntityExists(userId);

        public async Task Save() => await _repositoryWrapper.SaveAsync();

        public void Update(Folder model) => _repositoryWrapper.Folder.Update(model);

        public async Task<Folder> Create(Folder model)
        {
            await _repositoryWrapper.Folder.Create(model);

            return model;
        }

        public async Task<Folder> GetAllDataById(long id) => await _repositoryWrapper.Folder.GetAllDataFolder(u => u.Id == id);
    }
}
