using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.DAL.Interface;
using MyBookmarksAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyBookmarksAPI.DAL.Repository
{
    public class FolderRepository : RepositoryBase<Folder>, IFolderRepository
    {
        public FolderRepository(MyBookmarksDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Folder> GetAllDataFolder(Expression<Func<Folder, bool>> expression)
        {
            return await _dbSet
                .Include(f => f.Bookmarks)
                .AsNoTracking()
                .SingleOrDefaultAsync(expression);
        }
    }
}
