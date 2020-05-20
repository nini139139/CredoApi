using Domain.Dto.UserDto;
using Domain.Entity.UserEntity;
using Microsoft.EntityFrameworkCore;
using Repositor.Interface;
using Repositor.Interface.UserInterface;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repositor.Concrete.UserConcrete
{
    public  class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        private readonly IcommonRepository _icommon;

        public AuthRepository(DataContext context, IcommonRepository icommon)
        {
            _context = context;
            _icommon = icommon;
        }

        public async Task<Users> Login(UsersDto userForLogin)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userForLogin.UserName && x.RoleId== userForLogin.RoleId);
            if (user == null)
                return null;

            if (!VeryfyPasswordHash(userForLogin.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool VeryfyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for(int i=0; i<computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
            }

            return true;
        }

        public async Task<Users> Register(UsersDto user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var userToCreate = new Users
            {
                UserName = user.UserName,
                Name = user.Name,
                LastName = user.LastName,
                PasswordHash= passwordHash,
                PasswordSalt = passwordSalt,
                PersonalIdNumber = user.PersonalIdNumber,
                DateOfBirthday = user.DateOfBirthday,
                RoleId = user.RoleId
            };


            _icommon.Add(userToCreate);
            await _icommon.SaveAll();

            return userToCreate;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac= new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }

        }

        public async Task<bool> UserExists(string name)
        {
            if (await _context.Users.AnyAsync(x => x.Name == name))
                return true;

            return false;
        }
    }
}
