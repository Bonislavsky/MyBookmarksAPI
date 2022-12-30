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
        private static readonly string _password = "password";
        private static readonly byte[] UserSalt = HashPasswordSHA512.CreateSalt();
        private static readonly byte[] UserPassword = HashPasswordSHA512.HashPasswordSalt(_password, UserSalt);

        public static void Seed(this MyBookmarksDbContext dbContext)
        {
            dbContext.Users.AddRange(
                new User
                {
                    Id = 1,
                    Name = "Name1",
                    Email = "Email1@gmail.com",
                    Password = UserPassword,
                    Salt = UserSalt,
                    Folders = null
                },
                new User
                {
                    Id = 2,
                    Name = "Name2",
                    Email = "Email2@gmail.com",
                    Password = UserPassword,
                    Salt = UserSalt,
                    Folders = null
                },
                new User
                {
                    Id = 3,
                    Name = "Name3",
                    Email = "Email3@gmail.com",
                    Password = UserPassword,
                    Salt = UserSalt,
                    Folders = null
                }
             );

            dbContext.SaveChanges();
        }
    }
}
