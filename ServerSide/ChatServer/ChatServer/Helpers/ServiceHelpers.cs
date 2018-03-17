using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatServer.Helpers
{
    public class ServiceHelpers
    {
        internal static bool VerifyPasswordHash(string Password, byte[] StoredHash, byte[] StoredSalt)
        {
            if (Password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (StoredHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (StoredSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(StoredSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != StoredHash[i]) return false;
                }
            }

            return true;
        }
        internal static void CreatePasswordHash(string Password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            if (Password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(Password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }
    }
}
