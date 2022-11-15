using Microsoft.EntityFrameworkCore;
using MyBookmarksAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBookmarksAPI.DAL
{
    public class MyBookmarksDbContext : DbContext
    {
        public MyBookmarksDbContext(DbContextOptions<MyBookmarksDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
    }
}
