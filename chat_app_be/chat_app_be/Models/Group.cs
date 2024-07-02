namespace chat_app_be.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; } = string.Empty;
        public string GroupDescription { get; set; } = string.Empty;
    }
}
