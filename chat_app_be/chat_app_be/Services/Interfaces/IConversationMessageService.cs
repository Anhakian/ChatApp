using System;
using chat_app_be.Models.Response;

namespace chat_app_be.Services.Interfaces;

public interface IConversationMessageService
{
    Task<Response> GetConversationMessageByConversationId(int conversationId);
}
