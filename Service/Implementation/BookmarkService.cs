using MyBookmarksAPI.DAL.Wrapper;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Service.Interface;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Service.Implementation
{
    public class BookmarkService : IBookmarkService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public BookmarkService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper; 
        }

        public async Task Delete(long id) => _repositoryWrapper.Bookmark.Delete(await GetyById(id));

        public async Task<bool> EntityExists(long id) => await _repositoryWrapper.Bookmark.EntityExists(id);

        public Task<Bookmark> GetyById(long id)
        {
            throw new System.NotImplementedException();
        }

        public Task Save()
        {
            throw new System.NotImplementedException();
        }

        public void Update(Bookmark model)
        {
            throw new System.NotImplementedException();
        }
    }
}
