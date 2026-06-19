using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Helpers
{
    public static class JwtHelper
    {
        public static string GenerateToken(
            string userName,
            string secretKey)
        {
            var tokenHandler =
                new JwtSecurityTokenHandler();

            var key =
                Encoding.UTF8.GetBytes(secretKey);

            var tokenDescriptor =
                new SecurityTokenDescriptor
                {
                    Subject =
                        new ClaimsIdentity(
                            new[]
                            {
                            new Claim(
                                ClaimTypes.Name,
                                userName)
                            }),

                    Expires =
                        DateTime.UtcNow.AddHours(1),

                    SigningCredentials =
                        new SigningCredentials(
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256Signature)
                };

            var token =
                tokenHandler.CreateToken(
                    tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
