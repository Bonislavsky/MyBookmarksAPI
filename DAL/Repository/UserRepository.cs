using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.DAL.Interface;
using MyBookmarksAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.DAL.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private new readonly MyBookmarksDbContext _dbContext;
        public UserRepository(MyBookmarksDbContext dbContext) : base(dbContext) 
        {
            _dbContext = dbContext;
        }

        public override async Task<User> GetById(long id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
