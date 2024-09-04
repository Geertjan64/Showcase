using Microsoft.EntityFrameworkCore;
using Showcase.Models;

namespace Showcase.Data
{
    public class GameDbContext : DbContext
    {
        public GameDbContext(DbContextOptions<GameDbContext> options)
            : base(options)
        {
        }

        public DbSet<GameResultRecord> GameResults { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameResultRecord>()
                .HasIndex(e => e.GameId)
                .IsUnique(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}