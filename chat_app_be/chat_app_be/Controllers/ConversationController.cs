using Microsoft.AspNetCore.Mvc;
using chat_app_be.Dtos;
using chat_app_be.Services.Interfaces;

namespace chat_app_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationController : ControllerBase
    {
        private readonly IConversationService _conversationService;

        public ConversationController(IConversationService conversationService)
        {
            _conversationService = conversationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateConversation(ConversationRequestDto conversationRequest)
        {
            var result = await _conversationService.CreateConversation(conversationRequest);
            return result != null
                ? StatusCode(result.StatusCode, result)
                : StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }

        [HttpPut("updateConversationName")]
        public async Task<IActionResult> UpdateConversationName(int conversationId, string newConversationName)
        {
            var result = await _conversationService.UpdateConversationName(conversationId, newConversationName);
            return result != null
                ? StatusCode(result.StatusCode, result)
                : StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }
}