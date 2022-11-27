using AutoMapper;
using MyBookmarksAPI.Domain.DtoModel;
using MyBookmarksAPI.Domain.DtoModel.FolderDto;
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
            CreateMap<Folder, FolderWithBmDto>();
            CreateMap<Folder, FolderWithBmDto>().ReverseMap();

            CreateMap<Folder, FolderWithoutBmDto>();
            CreateMap<Folder, FolderWithoutBmDto>().ReverseMap();

            CreateMap<Folder, FolderUpdateDto>();
            CreateMap<Folder, FolderUpdateDto>().ReverseMap();

            CreateMap<Folder, FolderCreateDto>();
            CreateMap<Folder, FolderCreateDto>().ReverseMap();
        }
    }
}
