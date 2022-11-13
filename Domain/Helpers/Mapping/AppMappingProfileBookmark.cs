using AutoMapper;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Domain.TDOModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Domain.Helpers.Mapping
{
    public class AppMappingProfileBookmark : Profile
    {
        public AppMappingProfileBookmark()
        {
            CreateMap<Bookmark, BookmarkDto>();
            CreateMap<Bookmark, BookmarkDto>().ReverseMap();
        }  
    }
}
