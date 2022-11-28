using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MyBookmarksAPI.DAL.Interface
{
    public interface IFolderRepository : IRepositoryBase<Folder> 
    {
        Task<Folder> GetAllDataFolder(Expression<Func<Folder, bool>> expression);
    }
}
