using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyBookmarksAPI.Controllers;
using MyBookmarksAPI.DAL;
using MyBookmarksAPI.DAL.Wrapper;
using MyBookmarksAPI.Domain.DtoModel.UserDtoModel;
using MyBookmarksAPI.Domain.Helpers;
using MyBookmarksAPI.Domain.Helpers.Mapping;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Service;
using MyBookmarksAPI.Tests.Common;
using Xunit;

namespace MyBookmarksAPI.Tests.Controller
{
    public class UserControllerTest
    {
        private readonly Mapper _mapper = new Mapper(new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AppMappingProfileUser());
            cfg.AddProfile(new AppMappingProfileFolder());
            cfg.AddProfile(new AppMappingProfileBookmark());
        }));

        private UserController Arrange(MyBookmarksDbContext dbContext)
        {
            var wrapper = new RepositoryWrapper(dbContext);
            var userService = new UserService(wrapper);

            return new UserController(userService, _mapper);
        }

        [Fact]
        public async Task Get_ListUserAsync_ReturnsAllItems()
        {
            // Arrange
            var dbContext = UserContextFactory.Create(nameof(Get_ListUserAsync_ReturnsAllItems));
            var controller = Arrange(dbContext);

            // Act
            var OkResult = await controller.GetUsers() as OkObjectResult;
            var listUser = OkResult.Value as List<UserDto>;

            dbContext.Dispose();

            // Assert
            Assert.IsType<List<UserDto>>(listUser);
            Assert.Equal(DataForTest.userList.Count, listUser.Count);
        }

        [Theory]
        [InlineData(1, -1, long.MaxValue)]
        public async Task Get_UserById_ReturnsUser(long id1, long id2, long id3)
        {
            // Arrange
            var dbContext = UserContextFactory.Create(nameof(Get_UserById_ReturnsUser));
            var controller = Arrange(dbContext);

            // Act
            var OkResult = await controller.GetUser(id1) as OkObjectResult;
            var user = OkResult.Value as UserDto;

            dbContext.Dispose();

            // Assert
            Assert.True(user != null);
            Assert.Equal(id1, user.Id);          
        }
    }
}
