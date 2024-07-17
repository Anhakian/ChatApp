using chat_app_be.Dtos;
using chat_app_be.Models.Response;

namespace chat_app_be.Services.Interfaces
{
    public interface IUserService
    {
        Task<Response> Register(UserDto user);
        Task<Response> Login(UserRequestDto request);
    }
}
