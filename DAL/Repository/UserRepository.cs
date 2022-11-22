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
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MyBookmarksDbContext dbContext) : base(dbContext) 
        {
        }

        public override IQueryable<User> GetAll()
        {
            return _dbSet
                .Include(u => u.Folders)
                .ThenInclude(f=> f.BookMarks)
                .AsNoTracking();
        }

        public async Task<User> GetAllDataUser(Expression<Func<User, bool>> expression)
        {
            return await _dbSet
                .Include(u => u.Folders)
                .ThenInclude(f => f.BookMarks)
                .AsNoTracking()
                .SingleOrDefaultAsync(expression);
        }

        public async Task<bool> UserExists(long id) => await _dbSet.AnyAsync(u => u.Id == id);

        public async Task<bool> UserExists(string email) => await _dbSet.AnyAsync(u => u.Email.Equals(email));
    }
}
