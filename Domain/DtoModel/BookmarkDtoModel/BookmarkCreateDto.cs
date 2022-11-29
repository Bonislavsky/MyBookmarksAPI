using System.ComponentModel.DataAnnotations;

namespace MyBookmarksAPI.Domain.DtoModel.BookmarkDtoModel
{
    public class BookmarkCreateDto
    {
        [MaxLength(45, ErrorMessage = "довжина повинна бути до 45 символів")]
        public string Name { get; set; }
        public string Url { get; set; }

        public long FolderId { get; set; }
    }
}
