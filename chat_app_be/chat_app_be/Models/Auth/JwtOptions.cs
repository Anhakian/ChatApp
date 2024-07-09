namespace chat_app_be.Models.Auth
{
    public class JwtOptions
    {
        public string JwtKey { get; set; }
        public int JwtExpirationTime { get; set; }
    }
}
