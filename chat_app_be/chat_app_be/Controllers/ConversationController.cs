using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using Microsoft.Extensions.Logging;
using chat_app_be.Dtos;
using chat_app_be.Services.Interfaces;

namespace chat_app_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;
        private readonly ILogger<ConversationController> _logger;

        public ConversationController(IConversationService conversationService, ILogger<ConversationController> logger)
        {
            _conversationService = conversationService;
            _logger = logger;
        }

        [HttpPost("createConvo")]
        public async Task<IActionResult> CreateConversation(ConversationRequestDto conversationRequest)
        {
            _logger.LogInformation($"User.Identity.IsAuthenticated: {User.Identity.IsAuthenticated}");
            _logger.LogInformation($"User.Identity.Name: {User.Identity.Name}");
            foreach (var claim in User.Claims)
            {
                _logger.LogInformation($"Claim: {claim.Type} = {claim.Value}");
            }
            // Print the token
            string authHeader = HttpContext.Request.Headers["Authorization"].FirstOrDefault();
            _logger.LogInformation($"Authorization Header: {authHeader}");

            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                string token = authHeader.Substring("Bearer ".Length).Trim();
                _logger.LogInformation($"Token: {token}");
            }
            else
            {
                _logger.LogInformation("No Bearer token found in the request.");
            }

            // Your existing code
            var result = await _conversationService.CreateConversation(conversationRequest);
            return result != null
                ? StatusCode(result.StatusCode, result)
                : StatusCode(StatusCodes.Status500InternalServerError, "Something is wrong");
        }
    }
}