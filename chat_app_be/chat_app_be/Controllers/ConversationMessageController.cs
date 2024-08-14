using chat_app_be.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace chat_app_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationMessageController : ControllerBase
    {
        private readonly IConversationMessageService _conversationMessageService;
        public ConversationMessageController(IConversationMessageService conversationMessageService)
        {
            _conversationMessageService = conversationMessageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConversationMessageByConversationId(int conversationId)
        {
            var result = await _conversationMessageService.GetConversationMessageByConversationId(conversationId);
            return result != null
                ? StatusCode(result.StatusCode, result)
                : StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }
}
