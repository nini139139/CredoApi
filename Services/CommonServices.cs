using Domain.Entity.UserEntity;
using Domain.Enumerations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class CommonServices
    {

        public string Login(Users user)
        {
            var claims = new[]
          {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim("UserRole", user.RoleId.ToString())
            };
            var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("testtestetstestetstetetstetstejkresrerersrere"));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescription = new SecurityTokenDescriptor
            {

                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescription);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

        private bool VeryfyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }
        public static string GetStringValue(Enum value)
        {
            if (value == null)
            {
                return null;
            }
            string output = null;
            var type = value.GetType();
            var fi = type.GetField(value.ToString());
            var a = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];

            if (a.Length > 0)
            {
                output = a[0].Value;
            }

            return output;
        }



    }
}
