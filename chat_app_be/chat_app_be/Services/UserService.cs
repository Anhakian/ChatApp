using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using chat_app_be.Dtos;
using chat_app_be.Models;
using chat_app_be.Models.Auth;
using chat_app_be.Models.Response;
using chat_app_be.Services.Interfaces;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Logging;

namespace chat_app_be.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UserService> _logger;

        public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IConfiguration configuration, ILogger<UserService> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<Response> Register(UserDto user)
        {
            try
            {
                if (await _userManager.FindByNameAsync(user.UserName) != null)
                {
                    return new Response(StatusCodes.Status400BadRequest, "Username Already Exists");
                }

                var newUser = new User
                {
                    UserName = user.UserName,
                    DisplayName = user.DisplayName
                };

                var result = await _userManager.CreateAsync(newUser, user.Password);

                if (!result.Succeeded)
                {
                    return new Response(StatusCodes.Status400BadRequest, "User Registration Failed");
                }

                UserResponseDto userResponse = _mapper.Map<UserResponseDto>(newUser);

                return new Response(StatusCodes.Status200OK, userResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user registration");
                return new Response(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

        public async Task<Response> Login(UserRequestDto request)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(request.UserName);

                if (user == null)
                {
                    return new Response(StatusCodes.Status404NotFound, "User Not Found");
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (!result.Succeeded)
                {
                    return new Response(StatusCodes.Status400BadRequest, "Wrong Password");
                }

                string token = CreateToken(user);

                var response = new AuthDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    DisplayName = user.DisplayName,
                    Token = token,
                };

                return new Response(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during user login");
                return new Response(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

        private string CreateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:JwtKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:JwtExpirationTime"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<Response> GetUserById(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    return new Response(StatusCodes.Status404NotFound, "User Not Found");
                }

                UserResponseDto userResponse = _mapper.Map<UserResponseDto>(user);
                return new Response(StatusCodes.Status200OK, userResponse);
            }
            catch (Exception e)
            {
                return new Response(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }
    }
}
