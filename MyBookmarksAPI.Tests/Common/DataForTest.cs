using MyBookmarksAPI.Domain.Helpers;
using MyBookmarksAPI.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Tests.Common
{
    public static class DataForTest
    {
        private static string _password = "password";
        private static readonly byte[] _userSalt = HashPasswordSHA512.CreateSalt();
        private static readonly byte[] _userPassword = HashPasswordSHA512.HashPasswordSalt(_password, _userSalt);


        public static List<User> userList = new List<User>(){
                new User
                {
                    Id = 1,
                    Name = "Name_1",
                    Email = "Email1@gmail.com",
                    Password = _userPassword,
                    Salt = _userSalt
                },
                new User
                {
                    Id = 2,
                    Name = "Name_2",
                    Email = "Email2@gmail.com",
                    Password = _userPassword,
                    Salt = _userSalt
                },
                new User
                {
                    Id = 3,
                    Name = "Name_3",
                    Email = "Email3@gmail.com",
                    Password = _userPassword,
                    Salt = _userSalt,
                    Folders = null
                },
                new User
                {
                    Id = 4,
                    Name = "Name_4",
                    Email = "Email4@gmail.com",
                    Password = _userPassword,
                    Salt = _userSalt
                },
                new User
                {
                    Id = 5,
                    Name = "Name_5",
                    Email = "Email5@gmail.com",
                    Password = _userPassword,
                    Salt = _userSalt,
                    Folders = null
                }
        };

        public static List<Folder> folderList = new List<Folder>()
        {
            new Folder
            {
                Id = 1,
                Name = "UserId1_Name_1",
                UserId= 1,
                Bookmarks = null
            },
            new Folder
            {
                Id = 2,
                Name = "UserId1_Name_2",
                UserId= 1,
                Bookmarks = null
            },
            new Folder
            {
                Id = 3,
                Name = "UserId1_Name_3",
                UserId= 1,
            },
            new Folder
            {
                Id = 4,
                Name = "UserId2_Name_4",
                UserId= 2,
            },
            new Folder
            {
                Id = 5,
                Name = "UserId2_Name_5",
                UserId= 2,
            },
            new Folder
            {
                Id = 6,
                Name = "UserId4_Name_6",
                UserId= 4,
                Bookmarks = null
            },
            new Folder
            {
                Id = 7,
                Name = "UserId4_Name_7",
                UserId= 4,
            },
            new Folder
            {
                Id = 8,
                Name = "UserId4_Name_8",
                UserId= 4,
            },
        };

        public static List<Bookmark> bookmarkList = new List<Bookmark>()
        {
            new Bookmark
            {
                Id = 1,
                DateСreation = DateTime.UtcNow,
                Name = "Bookmark_1",
                Url = "https://www.google.com",
                FolderId = 3
            },
            new Bookmark
            {
                Id = 2,
                DateСreation = DateTime.UtcNow,
                Name = "Bookmark_2",
                Url = "https://www.google.com",
                FolderId = 3
            },
            new Bookmark
            {
                Id = 3,
                DateСreation = DateTime.UtcNow,
                Name = "Bookmark_3",
                Url = "https://www.google.com",
                FolderId = 3
            },
            new Bookmark
            {
                Id = 4,
                DateСreation = DateTime.UtcNow,
                Name = "Bookmark_4",
                Url = "https://www.google.com",
                FolderId = 4
            },
            new Bookmark
            {
                Id = 5,
                DateСreation = DateTime.UtcNow,
                Name = "Bookmark_5",
                Url = "https://www.google.com",
                FolderId = 4
            },
            new Bookmark
            {
                Id = 6,
                DateСreation = DateTime.UtcNow,
                Name = "Bookmark_6",
                Url = "https://www.google.com",
                FolderId = 5
            },
            new Bookmark
            {
                Id = 7,
                DateСreation = DateTime.UtcNow,
                Name = "Bookmark_7",
                Url = "https://www.google.com",
                FolderId = 7
            },
            new Bookmark
            {
                Id = 8,
                DateСreation = DateTime.UtcNow,
                Name = "Bookmark_8",
                Url = "https://www.google.com",
                FolderId = 7
            },
            new Bookmark
            {
                Id = 9,
                DateСreation = DateTime.UtcNow,
                Name = "Bookmark_9",
                Url = "https://www.google.com",
                FolderId = 7
            },
            new Bookmark
            {
                Id = 10,
                DateСreation = DateTime.UtcNow,
                Name = "Bookmark_10",
                Url = "https://www.google.com",
                FolderId = 8
            }
        };
    }
}
