using Konscious.Security.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoteManagementLab.Utils.Security
{
    internal class Cryptography
    {
        public string GetArgon2HashedPassword(string passwordToHash)
        {
            using(Argon2id hasher = new Argon2id(Encoding.UTF8.GetBytes(passwordToHash)))
            {
                byte[] salt = Encoding.UTF8.GetBytes("AOmWYhSx1CsYXx0I");
                hasher.DegreeOfParallelism = 2;
                hasher.MemorySize = 1024;
                hasher.Iterations = 2;
                hasher.Salt = salt;

                byte[] hashedBytes = hasher.GetBytes(64);
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool IsArgon2PasswordEqual(string clearPassword, string databaseHashedPassword)
        {
            string hashedProvidedPassword = GetArgon2HashedPassword(clearPassword);
            return hashedProvidedPassword.Equals(databaseHashedPassword);
        }
    }
}
