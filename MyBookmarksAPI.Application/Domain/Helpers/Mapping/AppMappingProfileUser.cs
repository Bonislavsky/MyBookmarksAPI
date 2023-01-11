using AutoMapper;
using MyBookmarksAPI.Domain.DtoModel.UserDtoModel;
using MyBookmarksAPI.Domain.Model;

namespace MyBookmarksAPI.Domain.Helpers.Mapping
{
    public class AppMappingProfileUser: Profile
    {
        public AppMappingProfileUser()
        {
            CreateMap<User, UserDto>();
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<User, UserUpdateDto>();
            CreateMap<User, UserUpdateDto>().ReverseMap();

            CreateMap<User, UserCreateDto>();
            CreateMap<User, UserCreateDto>().ReverseMap();

            CreateMap<User, UserAllDataDto>();
            CreateMap<User, UserAllDataDto>().ReverseMap();
        }
    }
}
