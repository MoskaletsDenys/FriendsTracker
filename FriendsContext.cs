using Microsoft.EntityFrameworkCore;

namespace FriendsTracker.Models
{
    public class FriendsContext:DbContext
    {
        public DbSet<Friend> Friends { get; set; }

        public FriendsContext(DbContextOptions<FriendsContext> options): base(options)
        {
            Database.EnsureCreated();
        }
    }
}
