using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace chat_app_be.Models
{
    public class User : IdentityUser
    {
        [MaxLength(30)]
        public string DisplayName {  get; set; } = string.Empty;
    }
}
