using ParcBack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ParcBack.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Zone> Zones => Set<Zone>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Zone>(b =>
        {
            b.ToTable("Zones");
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.Name).IsUnique(); 
            b.Property(x => x.Name).IsRequired().HasMaxLength(100);
        });
    }
}
