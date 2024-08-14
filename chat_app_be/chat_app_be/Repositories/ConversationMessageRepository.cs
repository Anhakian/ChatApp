using System;
using Cassandra;
using chat_app_be.Data;
using chat_app_be.Repositories.Interfaces;

namespace chat_app_be.Repositories;

public class ConversationMessageRepository : IConversationMessageRepository
{
    public readonly CassandraConfig _cassandraConfig;

    public ConversationMessageRepository(CassandraConfig cassandraConfig)
    {
        _cassandraConfig = cassandraConfig;
    }
    public async Task<ConversationMessage> AddConversationMessage(ConversationMessage message)
    {
        var session = _cassandraConfig.GetSession();

        var query = new SimpleStatement(
            "INSERT INTO chatapp.conversation_messages (conversation_id, timestamp, message_id, message, user_id) VALUES (?, ?, ?, ?, ?)",
            message.ConversationId,
            message.Timestamp,
            message.Id,
            message.Message,
            message.SenderId
        );

        await session.ExecuteAsync(query);

        return message;
    }

    public async Task<List<ConversationMessage>> GetConversationMessagesByConversationId(int conversationId)
    {
        var session = _cassandraConfig.GetSession();
        var query = new SimpleStatement(
            "SELECT * FROM chatapp.conversation_messages WHERE conversation_id = ?", conversationId);

        var resultSet = await session.ExecuteAsync(query);

        var messages = new List<ConversationMessage>();

        foreach (var row in resultSet.GetRows())
        {
            var message = new ConversationMessage
            {
                Id = row.GetValue<Guid>("message_id"),
                Message = row.GetValue<string>("message"),
                SenderId = row.GetValue<string>("user_id"),
                ConversationId = row.GetValue<int>("conversation_id"),
                Timestamp = row.GetValue<DateTime>("timestamp")
            };

            messages.Add(message);
        }

        return messages;
    }
}
