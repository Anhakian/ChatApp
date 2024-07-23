using System.ComponentModel.DataAnnotations;

namespace chat_app_be.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        [Required]
        public string ConversationName { get; set; } = string.Empty;
        [Required]
        public int User1Id { get; set; }
        [Required]
        public int User2Id { get; set; }
    }
}
