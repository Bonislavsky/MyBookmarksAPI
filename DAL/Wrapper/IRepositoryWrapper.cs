using MyBookmarksAPI.DAL.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.DAL.Wrapper
{
    public interface IRepositoryWrapper
    {
        IUserRepository User { get; init; }
        IFolderRepository Folder { get; init; }
        IBookmarkRepository Bookmark { get; init; }
        Task SaveAsync();
    }
}
