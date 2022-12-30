using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MyBookmarksAPI.Domain.Helpers
{
    public class HashPasswordSHA512
    {
        static readonly byte[] staticSalt = Encoding.UTF8.GetBytes("MyBM13");

        static public byte[] CreateSalt()
        {
            using var rng = new RNGCryptoServiceProvider();
            byte[] saltBytes = new byte[32];
            rng.GetNonZeroBytes(saltBytes);
            return saltBytes;
        }

        static public byte[] HashPasswordSalt(string password, byte[] salt)
        {
            using (var hashSha512 = SHA512.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] saltPlusPassword = salt
                    .Concat(passwordBytes.Concat(staticSalt)
                    .Concat(salt.Concat(staticSalt))
                    .Concat(passwordBytes))
                    .ToArray();

                return hashSha512.ComputeHash(saltPlusPassword);
            }
        }

        static public bool VerifyHash(string password, byte[] saltUser, byte[] hashPasswordUser)
        {
            var newHash = HashPasswordSalt(password, saltUser);
            return hashPasswordUser.SequenceEqual(newHash);
        }
    }
}
