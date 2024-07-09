using chat_app_be.Dtos;
using chat_app_be.Models;

namespace chat_app_be.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task AddUserAsync(User user);
        Task<bool> UserExistsAsync(string username);
    }
}
