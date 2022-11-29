using MyBookmarksAPI.Domain.DtoModel.BookmarkDtoModel;
using MyBookmarksAPI.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Service.Interface
{
    public interface IBookmarkService : IServiceBase<Bookmark> 
    {
        Task<List<Bookmark>> GetBookmarkList(long folderId, string sortParam, string AsdDec);
        Task<bool> FolderExists(long folderId);
        Task<Bookmark> Create(Bookmark model);
    }
}
