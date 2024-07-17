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
        [MaxLength(30)]
        public string DisplayName {  get; set; } = string.Empty;
        public byte[] PasswordHash {  get; set; }
        public byte[] PasswordSalt {  get; set; }  
    }
}
