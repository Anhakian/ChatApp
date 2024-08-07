using Cassandra;
using chat_app_be.Data;
using chat_app_be.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace chat_app_be;

public class ChatHub : Hub
{
    private readonly UserManager<User> _userManager;
    private readonly CassandraConfig _cassandraConfig;

    public ChatHub(UserManager<User> userManager, CassandraConfig cassandraConfig)
    {
        _userManager = userManager;
        _cassandraConfig = cassandraConfig;
    }

    public async Task JoinConversation(User user, Conversation conversation)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, conversation.ConversationName);
        await Clients.All.SendAsync("ReceiveMessage", $"{user.UserName} has joined {conversation.ConversationName}.");
    }

    public async Task SendMessage(int conversationId, string message, string senderId)
    {
        try
        {
            var messageId = Guid.NewGuid();
            var timestamp = DateTime.UtcNow;

            var newMessage = new ConversationMessage
            {
                Id = messageId,
                Message = message,
                SenderId = senderId,
                ConversationId = conversationId,
                Timestamp = timestamp
            };

            var query = new SimpleStatement(
                "INSERT INTO chatapp.conversation_messages (conversation_id, timestamp, message_id, message, user_id) VALUES (?, ?, ?, ?, ?)",
                conversationId, timestamp, messageId, message, senderId
            );

            await _cassandraConfig.GetSession().ExecuteAsync(query);

            await Clients.Group(conversationId.ToString()).SendAsync("ReceiveMessage", newMessage);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error sending message: {e.Message}");
        }
    }

}
