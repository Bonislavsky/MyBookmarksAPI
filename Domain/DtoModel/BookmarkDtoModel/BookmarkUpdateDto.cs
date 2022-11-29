using System;

namespace MyBookmarksAPI.Domain.DtoModel.BookmarkDtoModel
{
    public class BookmarkUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
