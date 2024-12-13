using DenemeFileOrbis.library.Models;
using Microsoft.EntityFrameworkCore;

namespace DenemeWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Client varlığı için yapılandırma
            modelBuilder.Entity<Client>(entity =>
            {
                entity.HasKey(e => e.Id); // 'Id' birincil anahtar
            });

            // User varlığı için yapılandırma
            modelBuilder.Entity<User>().HasNoKey();


        }
    }
}
