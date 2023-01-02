using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MyBookmarksAPI.Controllers;
using MyBookmarksAPI.DAL.Interface;
using MyBookmarksAPI.DAL.Wrapper;
using MyBookmarksAPI.Domain.DtoModel.UserDtoModel;
using MyBookmarksAPI.Domain.Helpers;
using MyBookmarksAPI.Domain.Helpers.Mapping;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Service;
using MyBookmarksAPI.Service.Interface;
using MyBookmarksAPI.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace MyBookmarksAPI.Tests.Controller
{
    public class UserControllerTest
    {
        private readonly MapperConfiguration _mapConfig = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AppMappingProfileUser());
            cfg.AddProfile(new AppMappingProfileFolder());
            cfg.AddProfile(new AppMappingProfileBookmark());
        });

        [Fact]
        public async Task Get_ListUserAsync_ReturnsAllItems()
        {
            // Arrange
            using var dbContext = UserContextFactory.Create(nameof(Get_ListUserAsync_ReturnsAllItems));

            var wrapper = new RepositoryWrapper(dbContext);
            var userService = new UserService(wrapper);
            var controller = new UserController(userService, new Mapper(_mapConfig));

            // Act
            var response = await controller.GetUsers();

            // Assert
            Assert.IsType<OkObjectResult>(response.Result);
            var list = response.Result as OkObjectResult;
            
            Assert.IsType<List<UserDto>>(list.Value);
            var listUser = list.Value as List<UserDto>;

            Assert.Equal(DataForTest.userList.Count, listUser.Count);
        }
    }
}
