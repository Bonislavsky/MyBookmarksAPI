using AutoMapper;
using MyBookmarksAPI.Domain.DtoModel.UserDtoModel;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Domain.TDOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Domain.Helpers.Mapping
{
    public class AppMappingProfileFolder : Profile
    {
        public AppMappingProfileFolder()
        {
            CreateMap<Folder, FolderDto>();
            CreateMap<Folder, FolderDto>().ReverseMap();
        }
    }
}
