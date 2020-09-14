using Microsoft.EntityFrameworkCore;

// TODO Move file to project root
namespace FriendsTracker.Models
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Friend> Friends { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
