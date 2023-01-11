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
using MyBookmarksAPI.Tests.Helpers;
using Xunit;

namespace MyBookmarksAPI.Tests.Controllers.User.Queries
{
    public class GetUserHandlerTest
    {
        [Fact]
        public async Task Get_ListUserAsync_ReturnsAllItems()
        {
            // Arrange
            using var dbContext = UserContextFactory.Create(nameof(Get_ListUserAsync_ReturnsAllItems));
            var controller = GetArrange.Arrange(dbContext);

            // Act
            var OkResult = await controller.GetUsers() as OkObjectResult;
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            var listUser = OkResult.Value as List<UserDto>;
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.

            // Assert
            Assert.IsType<List<UserDto>>(listUser);
            Assert.Equal(DataForTest.userList.Count, listUser.Count);
        }

        [Theory]
        [InlineData(1, -1, long.MaxValue)]
        public async Task Get_UserById_ReturnsUser(long id1, long id2, long id3)
        {
            // Arrange
            using var dbContext = UserContextFactory.Create(nameof(Get_UserById_ReturnsUser));
            var controller = GetArrange.Arrange(dbContext);

            // Act
            var OkResult = await controller.GetUser(id1) as OkObjectResult;
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            var user = OkResult.Value as UserDto;
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.

            var NotFound1 = await controller.GetUser(id2);
            var NotFound2 = await controller.GetUser(id3);

            // Assert
            Assert.IsType<UserDto>(user);
            Assert.True(user != null);
            Assert.Equal(id1, user.Id);

            Assert.IsType<NotFoundObjectResult>(NotFound1);
            Assert.IsType<NotFoundObjectResult>(NotFound2);
        }

        [Theory]
        [InlineData(1, -1, long.MaxValue)]
        public async Task Get_AllDataUserById_ReturnsUser(long id1, long id2, long id3)
        {
            // Arrange
            using var dbContext = UserContextFactory.Create(nameof(Get_AllDataUserById_ReturnsUser));
            var controller = GetArrange.Arrange(dbContext);

            // Act
            var OkResult = await controller.GetAllDataUser(id1) as OkObjectResult;
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
            var user = OkResult.Value as UserAllDataDto;
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.

            var NotFound1 = await controller.GetAllDataUser(id2);
            var NotFound2 = await controller.GetAllDataUser(id3);

            // Assert
            Assert.IsType<UserAllDataDto>(user);
            Assert.True(user != null);
            Assert.Equal(id1, user.Id);

            Assert.IsType<NotFoundObjectResult>(NotFound1);
            Assert.IsType<NotFoundObjectResult>(NotFound2);
        }


    }
}
