using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Dto.UserDto;
using Domain.Entity.UserEntity;
using Domain.Enumerations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositor.Interface.EnumInterface;
using Repositor.Interface.LogerInterface;
using Repositor.Interface.UserInterface;
using Services;

namespace CredoVacancy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly IConfiguration _config;
        private readonly IEnumRepository _enumRepo;
        private readonly ILoggerManager _logger;

        public AuthController(IAuthRepository authRepo, IConfiguration config, IEnumRepository enumRepo, ILoggerManager logger)
        {
            _authRepo = authRepo;
            _config = config;
            _enumRepo = enumRepo;
            _logger = logger;
        }

        [HttpPost("registerUser")]
        public async Task<IActionResult> RegisterUser([FromBody]UsersDto user)
        {
            _logger.LogInfo("request contorller:autcontroller.request func: RegisterUser ");
            user.UserName = user.UserName.ToLower();
            if (await _authRepo.UserExists(user.UserName))
            {
                return BadRequest("მომხმარებელი უკვე არსებობს");
            }


            var createdUser = await _authRepo.Register(user, user.Password);
            _logger.LogInfo("responce controller:autcontroller. responce func RegisterUser ");
            return StatusCode(201);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(UsersDto user)
        {
            _logger.LogInfo("request contorller:autcontroller.request func: Login ");
            var userFromRepo = await _authRepo.Login(user);

            if (userFromRepo == null)
            {
                _logger.LogError("user doesn't exist");
                return BadRequest("მომხმარებელი არ არსებობს");
            }




            CommonServices commonService = new CommonServices();

            var tokenHandler = new JwtSecurityTokenHandler();

            _logger.LogInfo("responce controller:autcontroller. responce func getRoleTypes ");

            return Ok(new
            {
                tokenHandler = commonService.Login(userFromRepo)
            });

        }


        [HttpGet("getRoleTypes")]
        public async Task<IActionResult> getRoleTypes()
        {
            _logger.LogInfo("request contorller:autcontroller.request func: getRoleTypes ");
            var roles =await _enumRepo.GetItems<RoleType>();

            _logger.LogInfo("responce controller:autcontroller. responce func getRoleTypes ");
            return Ok(roles);
        }

    }
}
