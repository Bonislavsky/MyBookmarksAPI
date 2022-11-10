using MyBookmarksAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.DAL.Interface
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<bool> UserExists(long id);
        Task<bool> UserExists(string email);
    }
}
