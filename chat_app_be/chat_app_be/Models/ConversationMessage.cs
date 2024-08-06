namespace chat_app_be;

public class ConversationMessage
{
    public Guid Id { get; set; }
    public string Message { get; set; } = string.Empty;
    public string SenderId { get; set; }
    public int ConversationId { get; set; }
    public DateTime Timestamp { get; set; }
}
