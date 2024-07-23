using chat_app_be.Dtos;
using chat_app_be.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using chat_app_be.Models.Auth;
using chat_app_be.Repositories.Interfaces;
using chat_app_be.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace chat_app_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDto user)
        {
            var result = await _userService.Register(user);
            return result != null
                ? StatusCode(result.StatusCode, result)
                : StatusCode(StatusCodes.Status500InternalServerError, "Something is wrong");
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserRequestDto request)
        {
            var result = await _userService.Login(request);
            return result != null
                ? StatusCode(result.StatusCode, result)
                : StatusCode(StatusCodes.Status500InternalServerError, "Something is wrong");
        }
    }
}
