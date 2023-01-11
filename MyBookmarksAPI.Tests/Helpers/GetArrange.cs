using AutoMapper;
using MyBookmarksAPI.Controllers;
using MyBookmarksAPI.DAL;
using MyBookmarksAPI.DAL.Wrapper;
using MyBookmarksAPI.Domain.Helpers.Mapping;
using MyBookmarksAPI.Service;

namespace MyBookmarksAPI.Tests.Helpers
{
    public static class GetArrange
    {
        private static readonly Mapper _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AppMappingProfileUser());
            cfg.AddProfile(new AppMappingProfileFolder());
            cfg.AddProfile(new AppMappingProfileBookmark());
        }));

        public static UserController Arrange(MyBookmarksDbContext dbContext)
        {
            var wrapper = new RepositoryWrapper(dbContext);
            var userService = new UserService(wrapper);

            return new UserController(userService, _mapper);
        }
    }
}
