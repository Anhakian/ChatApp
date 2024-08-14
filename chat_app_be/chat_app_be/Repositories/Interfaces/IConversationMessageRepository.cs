using System;

namespace chat_app_be.Repositories.Interfaces;

public interface IConversationMessageRepository
{
    Task<ConversationMessage> AddConversationMessage(ConversationMessage message);
    Task<List<ConversationMessage>> GetConversationMessagesByConversationId(int conversationId);
}
