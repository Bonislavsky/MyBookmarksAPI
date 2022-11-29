using AutoMapper;
using MyBookmarksAPI.Domain.DtoModel.BookmarkDtoModel;
using MyBookmarksAPI.Domain.Model;

namespace MyBookmarksAPI.Domain.Helpers.Mapping
{
    public class AppMappingProfileBookmark : Profile
    {
        public AppMappingProfileBookmark()
        {
            CreateMap<Bookmark, BookmarkDto>();
            CreateMap<Bookmark, BookmarkDto>().ReverseMap();

            CreateMap<Bookmark, BookmarkUpdateDto>();
            CreateMap<Bookmark, BookmarkUpdateDto>().ReverseMap();

            CreateMap<Bookmark, BookmarkCreateDto>();
            CreateMap<Bookmark, BookmarkCreateDto>().ReverseMap();
        }  
    }
}
