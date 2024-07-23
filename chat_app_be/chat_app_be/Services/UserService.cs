using chat_app_be.Models.Auth;
using chat_app_be.Models;
using chat_app_be.Repositories.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using chat_app_be.Dtos;
using chat_app_be.Models.Response;
using AutoMapper;
using chat_app_be.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic;
using System.Data;

namespace chat_app_be.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IOptions<JwtOptions> jwtOptions, IMapper mapper)
        {
            _userRepository = userRepository;
            _jwtOptions = jwtOptions;
            _mapper = mapper;
        }

        public async Task<Response> Register(UserDto user)
        {
            try
            {
                if (await _userRepository.IsUserExist(user.Username))
                {
                    return new Response(statusCodes: StatusCodes.Status400BadRequest, "Username Already Exists");
                }

                CreatePasswordHash(user.Password, out byte[] passwordHash, out byte[] passwordSalt);

                var newUser = new User
                {
                    Username = user.Username,
                    DisplayName = user.DisplayName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                await _userRepository.AddUserAsync(newUser);

                UserResponseDto userResponse = _mapper.Map<UserResponseDto>(newUser);

                return new Response(statusCodes: StatusCodes.Status200OK, userResponse);
            }
            catch (Exception e)
            {
                return new Response(statusCodes: StatusCodes.Status500InternalServerError, "Something is Wrong");
            }
        }

        public async Task<Response> Login(UserRequestDto request)
        {
            try
            {
                var user = await _userRepository.GetUserByUsernameAsync(request.Username);

                if (user == null)
                {
                    return new Response(statusCodes: StatusCodes.Status404NotFound, "User Not Found"); 
                }

                if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return new Response(statusCodes: StatusCodes.Status400BadRequest, "Wrong Password"); 
                }

                string token = CreateToken(user);

                var response = new AuthDto
                {
                    Username = user.Username,
                    DisplayName = user.DisplayName,
                    Token = token,
                };

                return new Response(statusCodes: StatusCodes.Status200OK, data: response);
            }
            catch (Exception e)
            {
                return new Response(statusCodes: StatusCodes.Status500InternalServerError, "Something is Wrong");
            }
        }

        private string CreateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtOptions.Value.JwtExpirationTime),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
