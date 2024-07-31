using AutoMapper;
using chat_app_be.Dtos;
using chat_app_be.Models;
using chat_app_be.Models.Response;
using chat_app_be.Repositories.Interfaces;
using chat_app_be.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace chat_app_be.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ConversationService> _logger;

        public ConversationService(IConversationRepository conversationRepository, UserManager<User> userManager, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<ConversationService> logger)
        {
            _conversationRepository = conversationRepository;
            _userManager = userManager;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<Response> CreateConversation(ConversationRequestDto conversationRequest)
        {
            try
            {
                var user1 = await _userManager.FindByNameAsync(conversationRequest.UserName1);
                if (user1 == null)
                {
                    return new Response(StatusCodes.Status404NotFound, "Current User Not Found");
                }

                var user2 = await _userManager.FindByNameAsync(conversationRequest.UserName2);
                if (user2 == null)
                {
                    return new Response(StatusCodes.Status404NotFound, "User Not Found");
                }

                var existingConversation = await _conversationRepository.GetConversationByUsers(user1.Id, user2.Id);
                if (existingConversation != null)
                {
                    return new Response(StatusCodes.Status409Conflict, "Conversation already exists between these users");
                }

                var conversation = new Conversation
                {
                    ConversationName = conversationRequest.ConversationName,
                    User1Id = user1.Id,
                    User2Id = user2.Id
                };

                await _conversationRepository.CreateConversation(conversation);
                return new Response(StatusCodes.Status200OK, $"You have successfully created a conversation with {user2.UserName}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating conversation");
                return new Response(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }

        public async Task<Response> UpdateConversationName(int conversationId, string newConversationName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(newConversationName))
                {
                    return new Response(StatusCodes.Status400BadRequest, $"New conversation name cannot be empty");
                }

                var existingConversation = await _conversationRepository.GetConversationById(conversationId);
                if (existingConversation == null)
                {
                    return new Response(StatusCodes.Status404NotFound, "Conversation not found");
                }

                existingConversation.ConversationName = newConversationName;

                await _conversationRepository.UpdateConversation(existingConversation);

                return new Response(StatusCodes.Status200OK, $"The conversation name has been updated to {existingConversation.ConversationName}");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error renaming conversation");
                return new Response(StatusCodes.Status500InternalServerError, "Something went wrong");
            }
        }
    }
}
