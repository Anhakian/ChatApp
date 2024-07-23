using chat_app_be.Data;
using chat_app_be.Models;
using chat_app_be.Repositories.Interfaces;

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
    }
}
