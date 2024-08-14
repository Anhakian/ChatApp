using System;
using AutoMapper;
using chat_app_be.Dtos;
using chat_app_be.Models;
using chat_app_be.Models.Response;
using chat_app_be.Repositories.Interfaces;
using chat_app_be.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace chat_app_be.Services;

public class ConversationMessageService : IConversationMessageService
{
    private readonly IConversationMessageRepository _conversationMessageRepository;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;


    public ConversationMessageService(IConversationMessageRepository conversationMessageRepository, IMapper mapper, UserManager<User> userManager)
    {
        _conversationMessageRepository = conversationMessageRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<Response> GetConversationMessageByConversationId(int conversationId)
    {
        try
        {
            var conversationMessages = await _conversationMessageRepository.GetConversationMessagesByConversationId(conversationId);
            var conversationMessagesDto = new List<ConversationMessageDto>();

            foreach (var message in conversationMessages)
            {
                var sender = await _userManager.FindByIdAsync(message.SenderId);

                var messageDto = _mapper.Map<ConversationMessageDto>(message);
                messageDto.SenderDisplayName = sender.DisplayName;

                conversationMessagesDto.Add(messageDto);
            }

            return new Response(StatusCodes.Status200OK, "Successfully retrieved conversation messages", conversationMessagesDto);
        }
        catch (Exception e)
        {
            return new Response(StatusCodes.Status500InternalServerError, "Something went wrong", e);
        }
    }
}
