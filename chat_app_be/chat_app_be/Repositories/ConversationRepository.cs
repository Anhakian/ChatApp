using chat_app_be.Data;
using chat_app_be.Models;
using chat_app_be.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace chat_app_be.Repositories
{
    public class ConversationRepository : IConversationRepository
    {
        private readonly ApplicationDbContext _context;

        public ConversationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Conversation> CreateConversation(Conversation conversation)
        {
            try
            {
                await _context.AddAsync(conversation);
                await _context.SaveChangesAsync();
                return conversation;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Conversation?> GetConversationByUsers(string user1Id, string user2Id)
        {
            try
            {
                return await _context.Conversations
                    .FirstOrDefaultAsync(c =>
                        (c.User1Id == user1Id && c.User2Id == user2Id) ||
                        (c.User1Id == user2Id && c.User2Id == user1Id));
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public async Task<List<Conversation>> GetConversationsByUserId(string userId)
        {
            try
            {
                return await _context.Conversations
                    .Where(c => c.User1Id == userId || c.User2Id == userId)
                    .ToListAsync();
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Conversation?> GetConversationById(int Id)
        {
            try
            {
                return await _context.Conversations.FindAsync(Id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task UpdateConversation(Conversation conversation)
        {
            try
            {
                _context.Conversations.Update(conversation);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return;
            }
        }
    }
}
