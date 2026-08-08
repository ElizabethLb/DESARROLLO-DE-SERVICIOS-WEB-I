using Microsoft.EntityFrameworkCore;

namespace BookMatch.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Add DbSet<T> properties here, for example:
        // public DbSet<User> Users { get; set; }
    }
}
