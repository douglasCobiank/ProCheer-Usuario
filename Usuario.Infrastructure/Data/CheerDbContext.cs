using Microsoft.EntityFrameworkCore;
using Usuario.Infrastructure.Data.Models;

namespace Usuario.Infrastructure.Data
{
    public class CheerDbContext : DbContext
    {
        public CheerDbContext(DbContextOptions<CheerDbContext> options)
            : base(options) { }

        public DbSet<UsuarioData> Usuario { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // garante explicitamente a PK (útil se o nome for diferente)
            modelBuilder.Entity<UsuarioData>(entity =>
            {
                entity.HasKey(c => c.UsuarioId);
                entity.Property(e => e.UsuarioId).ValueGeneratedOnAdd();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}