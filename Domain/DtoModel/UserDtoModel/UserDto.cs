using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Domain.TDOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyBookmarksAPI.Domain.DtoModel;
using MyBookmarksAPI.Domain.DtoModel.FolderDto;

namespace MyBookmarksAPI.Domain.DtoModel.UserDtoModel
{
    public class UserDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<FolderWithoutBmDto> Folders { get; set; }
    }
}
