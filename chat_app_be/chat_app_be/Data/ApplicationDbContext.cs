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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conversation>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.User1Id)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Conversation>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.User2Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Ensure User1Id and User2Id cannot point to the same user
            modelBuilder.Entity<Conversation>()
                .HasCheckConstraint("CK_UserIds", "[User1Id] <> [User2Id]");

            base.OnModelCreating(modelBuilder);
        }
    }
}
