using MyBookmarksAPI.DAL.Interface;
using MyBookmarksAPI.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.DAL.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private MyBookmarksDbContext _dbContext;

        public IUserRepository User { get; init; }
        public IFolderRepository Folder { get; init; }
        public IBookmarkRepository Bookmark { get; init; }

        public RepositoryWrapper(MyBookmarksDbContext dbContext)
        {
            _dbContext = dbContext;
            User = new UserRepository(dbContext);
            Folder = new FolderRepository(dbContext);
            Bookmark = new BookmarkRepository(dbContext);
        }

        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();
    }
}
