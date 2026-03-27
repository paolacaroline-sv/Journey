using Microsoft.EntityFrameworkCore;

namespace Journey.Infrastructure
{
    public class JourneyDbContext : DbContext
    {
        public JourneyDbContext(DbContextOptions<JourneyDbContext> options) : base(options)
        {
        }

        public DbSet<Entities.Trip> Trips { get; set; }
    }
}