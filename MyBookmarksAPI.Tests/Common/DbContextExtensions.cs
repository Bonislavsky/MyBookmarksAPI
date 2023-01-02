using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.DAL;
using MyBookmarksAPI.Domain.Helpers;
using MyBookmarksAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Tests.Common
{
    public static class DbContextExtensions
    {
        public static void Seed(this MyBookmarksDbContext dbContext)
        {
            dbContext.Users.AddRange(DataForTest.userList);
            dbContext.Folders.AddRange(DataForTest.folderList);
            dbContext.Bookmarks.AddRange(DataForTest.bookmarkList);

            dbContext.SaveChanges();
        }
    }
}
