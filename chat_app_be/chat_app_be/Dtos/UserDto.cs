using System.ComponentModel.DataAnnotations;

namespace chat_app_be.Dtos
{
    public class UserDto
    {
        [Required]
        [MaxLength(30)]
        public string Username { get; set; } = string.Empty;
        [MaxLength(30)]
        public string DisplayName { get; set; } = string.Empty;
        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;
    }

    public class UserRequestDto
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
