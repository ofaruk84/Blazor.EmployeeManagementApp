using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Lib.Utilities
{
    public static class BcryptHasher
    {
        public static string HashPassword(string password)
        {
            if (password is null)
            {
                throw new ArgumentNullException("password");
            }

            return BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
