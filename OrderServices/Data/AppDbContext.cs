using Microsoft.EntityFrameworkCore;

namespace OrderServices.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Entities.Order> Orders { get; set; }
    }
}
