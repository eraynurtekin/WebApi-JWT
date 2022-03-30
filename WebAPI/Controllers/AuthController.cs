using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:Controller
    {
        private IAuthService _authservice;
        public AuthController(IAuthService authService)
        {
            _authservice = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(UserForLoginDto userForLoginDto)
        {
            var userToLogin = _authservice.Login(userForLoginDto);
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }
            var result = _authservice.CreateAccessToken(userToLogin.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        [HttpPost("register")]
        public IActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authservice.UserExists(userForRegisterDto.Email);

            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = _authservice.Register(userForRegisterDto,userForRegisterDto.Password);
            var result = _authservice.CreateAccessToken(registerResult.Data);

            if (result.Success)
            {
                return Ok(result.Data); //Token üretildi
            }
            return BadRequest(result.Message);
            
        }


    }
}
