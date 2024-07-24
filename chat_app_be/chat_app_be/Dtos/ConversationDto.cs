namespace chat_app_be.Dtos
{
    public class ConversationRequestDto
    {
        public string ConversationName { get; set; } = string.Empty;
        public string UserName2 { get; set; } = string.Empty;
    }

    public class ConversationResponseDto
    {
        public string ConversationName { get; set; } = string.Empty;
    }
    
}
