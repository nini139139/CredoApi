using Domain.Dto.UserDto;
using Domain.Entity.UserEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositor.Interface.UserInterface
{
   public interface IAuthRepository
    {
        Task<Users> Register(UsersDto user, string password);
        Task<Users> Login(UsersDto userForLogin);
        Task<bool> UserExists(string name);

    }
}
