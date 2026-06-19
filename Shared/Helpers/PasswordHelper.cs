using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public static class PasswordHelper
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(
            string password,
            string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(
                password,
                hashPassword);
        }
    }
}
