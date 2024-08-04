using chat_app_be.Models;

namespace chat_app_be.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> AddUserAsync(User user);
        Task<bool> IsUserExist(string username);
    }
}
