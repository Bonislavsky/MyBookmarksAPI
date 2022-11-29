using MyBookmarksAPI.Domain.DtoModel.BookmarkDtoModel;
using System.Collections.Generic;

namespace MyBookmarksAPI.Domain.DtoModel.FolderDtoModel
{
    public class FolderWithBmDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long UserId { get; set; }

        public List<BookmarkDto> Bookmarks { get; set; }
    }
}
