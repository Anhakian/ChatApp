using AutoMapper;
using chat_app_be.Dtos;
using chat_app_be.Models;
using chat_app_be.Models.Response;
using chat_app_be.Repositories.Interfaces;
using chat_app_be.Services.Interfaces;
using System.Security.Claims;

namespace chat_app_be.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IConversationRepository _conversationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<ConversationService> _logger;

        public ConversationService(IConversationRepository conversationRepository, IUserRepository userRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, ILogger<ConversationService> logger)
        {
            _conversationRepository = conversationRepository;
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<Response> CreateConversation(ConversationRequestDto conversationRequest)
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                _logger.LogInformation($"HttpContext: {httpContext != null}");

                if (httpContext != null)
                {
                    _logger.LogInformation($"User: {httpContext.User != null}");
                    _logger.LogInformation($"User.Identity: {httpContext.User.Identity != null}");
                    _logger.LogInformation($"User.Identity.IsAuthenticated: {httpContext.User.Identity?.IsAuthenticated}");

                    var claims = httpContext.User.Claims.Select(c => $"{c.Type}: {c.Value}");
                    _logger.LogInformation($"Claims: {string.Join(", ", claims)}");
                }

                var currentUsername = httpContext?.User.FindFirst(ClaimTypes.Name)?.Value;
                _logger.LogInformation($"Current username: {currentUsername}"); if (currentUsername == null)
                {
                    return new Response(StatusCodes.Status401Unauthorized, "Unauthorized");
                }

                User user1 = await _userRepository.GetUserByUsernameAsync(currentUsername);
                User user2 = await _userRepository.GetUserByUsernameAsync(conversationRequest.Username2);
                if (user2 == null)
                {
                    return new Response(statusCodes: StatusCodes.Status404NotFound, "User Not Found");
                }
                
                var conversation = new Conversation
                {
                    ConversationName = conversationRequest.ConversationName,
                    User1Id = user1.Id,
                    User2Id = user2.Id
                };

                await _conversationRepository.CreateConversation(conversation);
                return new Response(statusCodes: StatusCodes.Status200OK, message: $"You have successfully create a conversation with {user2.Username}");
            }
            catch (Exception e)
            {
                return new Response(statusCodes: StatusCodes.Status500InternalServerError, "Something is Wrong");
            }
        }
    }
}
