using chat_app_be.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace chat_app_be.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
    }
}
