using System.ComponentModel.DataAnnotations;

namespace chat_app_be.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Username { get; set; } = string.Empty;
        public string DisplayName {  get; set; } = string.Empty;
        [Required]
        [MinLength(8)]
        public string Password {  get; set; } = string.Empty;
    }
}
