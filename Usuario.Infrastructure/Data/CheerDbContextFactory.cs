using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Usuario.Infrastructure.Data
{
    public class CheerDbContextFactory : IDesignTimeDbContextFactory<CheerDbContext>
    {
        public CheerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CheerDbContext>();

            optionsBuilder.UseNpgsql("Host=db.tugjkfhjgfzqttuexicr.supabase.co;Database=postgres;Username=postgres;Password=procheer2025;SSL Mode=Require;Trust Server Certificate=true");

            return new CheerDbContext(optionsBuilder.Options);
        }
    }
}