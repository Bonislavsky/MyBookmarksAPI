using MyBookmarksAPI.Domain.DtoModel.FolderDtoModel;
using System.Collections.Generic;

namespace MyBookmarksAPI.Domain.DtoModel.UserDtoModel
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
