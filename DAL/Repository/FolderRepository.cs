using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.DAL.Interface;
using MyBookmarksAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.DAL.Repository
{
    public class FolderRepository : RepositoryBase<Folder>, IFolderRepository
    {
        private new readonly MyBookmarksDbContext _dbContext;
        public FolderRepository(MyBookmarksDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<Folder> GetById(long id)
        {
            return await _dbContext.Folders.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
