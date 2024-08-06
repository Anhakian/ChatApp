using chat_app_be.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace chat_app_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _userService.GetUserById(id);
            return result != null
                ? StatusCode(result.StatusCode, result)
                : StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong");
        }
    }
}