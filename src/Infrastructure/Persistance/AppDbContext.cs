using ParcBack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ParcBack.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Zone> Zones => Set<Zone>();
    public DbSet<Ride> Rides => Set<Ride>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Zone>(b =>
        {
            b.ToTable("Zones");
            b.HasKey(x => x.Id);
            b.HasIndex(x => x.Theme).IsUnique(); 
            b.Property(x => x.Theme).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Ride>(b =>
        {
            b.ToTable("Rides");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).IsRequired().HasMaxLength(100);
            b.HasOne(x => x.Zone)
             .WithMany()
             .HasForeignKey("ZoneId")
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);
        });
    }
}
