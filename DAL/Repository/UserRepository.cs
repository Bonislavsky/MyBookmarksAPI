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
        public UserRepository(MyBookmarksDbContext dbContext) : base(dbContext) 
        {
        }

        public Task<bool> UserExists(long id) => _dbSet.AnyAsync(u => u.Id == id);

        public Task<bool> UserExists(string email) => _dbSet.AnyAsync(u => u.Email.Equals(email));
    }
}
