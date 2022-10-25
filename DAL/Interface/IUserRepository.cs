using MyBookmarksAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.DAL.Interface
{
    interface IUserRepository : IRepositoryBase<User>
    {
    }
}
