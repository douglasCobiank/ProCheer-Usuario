using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Usuario.Infrastructure.Data
{
    public class CheerDbContextFactory : IDesignTimeDbContextFactory<CheerDbContext>
    {
        public CheerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CheerDbContext>();

            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=procampdb;Username=postgres;Password=postgres;KeepAlive=30");

            return new CheerDbContext(optionsBuilder.Options);
        }
    }
}