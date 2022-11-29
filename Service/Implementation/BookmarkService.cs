using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.DAL.Wrapper;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Service.Implementation
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public BookmarkService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper; 
        }

        public async Task<bool> EntityExists(long id) => await _repositoryWrapper.Bookmark.EntityExists(u => u.Id == id);

        public async Task<bool> FolderExists(long folderId) => await _repositoryWrapper.Folder.EntityExists(u => u.Id == folderId);

        public async Task<List<Bookmark>> GetBookmarkList(long folderId, string sortParam, string AsdDec)
        {
            return await _repositoryWrapper.Bookmark.GetListByCondition(f => f.FolderId == folderId).OrderBy($"{sortParam} {AsdDec}").ToListAsync();
        }

        public async Task<Bookmark> GetyById(long id) => await _repositoryWrapper.Bookmark.GetByCondition(u => u.Id == id);

        public async Task Save() => await _repositoryWrapper.SaveAsync();

        public async Task Delete(long id) => _repositoryWrapper.Bookmark.Delete(await GetyById(id));

        public void Update(Bookmark model) => _repositoryWrapper.Bookmark.Update(model);

        public async Task<Bookmark> Create(Bookmark model)
        {
            int maxLength = 45;

            if (string.IsNullOrWhiteSpace(model.Name))
            {
                model.Name = model.Url.Substring(0, Math.Min(model.Url.Length, maxLength));
            }

            await _repositoryWrapper.Bookmark.Create(model);

            return model;
        }
    }
}
