using FaSoLaSearch.Models;
using Microsoft.EntityFrameworkCore;

namespace FaSoLaSearch.Data
{
    public class PartContext : DbContext
    {
        public PartContext(DbContextOptions<PartContext> options)
            : base(options) { }

        public DbSet<Part> Parts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Part>(entity =>
            {
                entity.HasKey(e => e.PartId);
                entity.Property(e => e.SongNumber).IsRequired();
                entity.Property(e => e.SongName).IsRequired();
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.First).IsRequired();
                entity.Property(e => e.Second).IsRequired();
                entity.Property(e => e.Third).IsRequired();
                entity.Property(e => e.Fourth).IsRequired();
                entity.Property(e => e.Fifth).IsRequired();
                entity.Property(e => e.Sixth).IsRequired();
                entity.Property(e => e.Seventh).IsRequired();
                entity.Property(e => e.Eighth).IsRequired();
            });
        }
    }
}
