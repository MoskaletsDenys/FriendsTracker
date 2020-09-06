using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// TODO Move file to project root
namespace FriendsTracker.Models
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Friend> Friends { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
