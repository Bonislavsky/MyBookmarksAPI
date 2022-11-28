using MyBookmarksAPI.Domain.DtoModel.FolderDto;
using System.Collections.Generic;

namespace MyBookmarksAPI.Domain.DtoModel.UserDtoModel
{
    public class UserAllDataDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<FolderWithBmDto> Folders { get; set; }
    }
}
