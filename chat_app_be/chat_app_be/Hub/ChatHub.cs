using AutoMapper;
using Cassandra;
using chat_app_be.Data;
using chat_app_be.Dtos;
using chat_app_be.Models;
using chat_app_be.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace chat_app_be;

public class ChatHub : Hub
{
    private readonly UserManager<User> _userManager;
    private readonly CassandraConfig _cassandraConfig;
    private readonly IMapper _mapper;
    private readonly IConversationRepository _conversationRepository;
    private readonly IConversationMessageRepository _conversationMessageRepository;

    public ChatHub(UserManager<User> userManager, CassandraConfig cassandraConfig, IMapper mapper, IConversationRepository conversationRepository, IConversationMessageRepository conversationMessageRepository)
    {
        _userManager = userManager;
        _cassandraConfig = cassandraConfig;
        _mapper = mapper;
        _conversationRepository = conversationRepository;
        _conversationMessageRepository = conversationMessageRepository;
    }

    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public async Task JoinConversation(int conversationId, string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user != null)
        {
            var conversation = await _conversationRepository.GetConversationById(conversationId);

            if (conversation != null)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, conversation.ConversationName);
                await Clients.Group(conversation.ConversationName).SendAsync("ReceiveMessage", $"{user.UserName} has joined {conversation.ConversationName}.");
            }
        }
    }

    public async Task SendMessage(int conversationId, string message, string senderId)
    {
        try
        {
            var response = await _conversationRepository.GetConversationById(conversationId);
            var conversationName = response.ConversationName;

            var messageId = Guid.NewGuid();
            var timestamp = DateTime.UtcNow;

            var sender = await _userManager.FindByIdAsync(senderId);
            if (sender == null)
                throw new Exception($"User with ID {senderId} not found");

            var senderDisplayName = sender.DisplayName;

            var newMessage = new ConversationMessage
            {
                Id = messageId,
                Message = message,
                SenderId = senderId,
                ConversationId = conversationId,
                Timestamp = timestamp
            };

            await _conversationMessageRepository.AddConversationMessage(newMessage);

            var newMessageDto = _mapper.Map<ConversationMessageDto>(newMessage);
            newMessageDto.SenderDisplayName = senderDisplayName;

            await Clients.Group($"{conversationName}").SendAsync("ReceiveMessage", newMessageDto);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in SendMessage: {e.Message}");
            Console.WriteLine($"Stack Trace: {e.StackTrace}");
            throw;
        }
    }

}
