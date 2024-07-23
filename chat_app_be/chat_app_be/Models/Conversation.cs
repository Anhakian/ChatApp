using System.ComponentModel.DataAnnotations;

namespace chat_app_be.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        [Required]
        public string ConversationName { get; set; } = string.Empty;
        public string ConversationDescription { get; set; } = string.Empty;
    }
}
