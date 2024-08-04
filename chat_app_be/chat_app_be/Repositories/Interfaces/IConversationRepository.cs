using chat_app_be.Models;

namespace chat_app_be.Repositories.Interfaces
{
    public interface IConversationRepository
    {
        Task<Conversation> CreateConversation(Conversation conversation);
        Task<Conversation?> GetConversationByUsers(string user1Id, string user2Id);
        Task<List<Conversation>> GetConversationsByUserId(string userId);
        Task<Conversation?> GetConversationById(int id);
        Task UpdateConversation(Conversation conversation);
    }
}
