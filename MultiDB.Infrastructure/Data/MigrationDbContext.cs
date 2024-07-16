using Microsoft.EntityFrameworkCore;

namespace MultiDB.Infrastructure.Data
{
    public class MigrationDbContext : ApplicationDbContext
    {
        public MigrationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
}
