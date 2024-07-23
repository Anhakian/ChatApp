using chat_app_be.Models;

namespace chat_app_be.Repositories.Interfaces
{
    public interface IConversationRepository
    {
        Task<Conversation> CreateConversation(Conversation conversation);
    }
}
