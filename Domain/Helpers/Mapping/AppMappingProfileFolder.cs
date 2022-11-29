using AutoMapper;
using MyBookmarksAPI.Domain.DtoModel.FolderDtoModel;
using MyBookmarksAPI.Domain.Model;

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
