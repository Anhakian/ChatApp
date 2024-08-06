using chat_app_be.Models;
using chat_app_be.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace chat_app_be.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<User> AddUserAsync(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<bool> IsUserExist(string username)
        {
            return await _context.Users.AnyAsync(u => u.UserName == username);
        }

        public async Task<User> GetUserById(string id)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
