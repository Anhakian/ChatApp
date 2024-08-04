namespace chat_app_be.Dtos
{
    public class AuthDto
    {
        public string UserName { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
