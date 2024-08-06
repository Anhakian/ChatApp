using chat_app_be.Dtos;
using chat_app_be.Models.Response;

namespace chat_app_be.Services.Interfaces
{
    public interface IConversationService
    {
        Task<Response> CreateConversation(ConversationRequestDto conversationRequest);
        Task<Response> UpdateConversationName(int conversationId, string newConversationName);
        Task<Response> GetConversationsByUserId(string userId);
    }
}
