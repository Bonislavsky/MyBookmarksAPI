using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.Controllers;
using MyBookmarksAPI.DAL;
using MyBookmarksAPI.DAL.Wrapper;
using MyBookmarksAPI.Domain.Helpers;
using MyBookmarksAPI.Domain.Model;
using MyBookmarksAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Tests.Common
{
    public class UserContextFactory
    {
        public static MyBookmarksDbContext GetWideWorldImportersDbContext(string dbName)
        {
            // Create options for DbContext instance
            var options = new DbContextOptionsBuilder<MyBookmarksDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;

            // Create instance of DbContext
            var dbContext = new MyBookmarksDbContext(options);

            // Add entities in memory
            dbContext.Seed();

            return dbContext;
        }
    }
}
