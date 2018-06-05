using Microsoft.EntityFrameworkCore;

namespace AdvWebUtvProj.Data
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Thing> Things { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> context) : base(context)
        {

        }
    }
}