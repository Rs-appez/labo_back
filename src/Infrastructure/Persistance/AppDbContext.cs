using ParcBack.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ParcBack.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Zone> Zones => Set<Zone>();
    public DbSet<Ride> Rides => Set<Ride>();

    public DbSet<Employee> Employees => Set<Employee>();
    public DbSet<Chief> Chiefs => Set<Chief>();
    public DbSet<Role> Roles => Set<Role>();

    public DbSet<TaskType> TaskTypes => Set<TaskType>();

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

        modelBuilder.Entity<Role>(b =>
        {
            b.ToTable("Roles");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).IsRequired().HasMaxLength(50);
            b.HasIndex(x => x.Name).IsUnique();
        });

        modelBuilder.Entity<Role>().HasData(
            new Role { Id = 1, Name = "Admin" },
            new Role { Id = 2, Name = "Chief" },
            new Role { Id = 3, Name = "Employee" }
        );

        modelBuilder.Entity<Employee>(b =>
        {
            b.ToTable("Employees");
            b.HasKey(x => x.Id);
            b.Property(x => x.Email).IsRequired().HasMaxLength(100);
            b.HasIndex(x => x.Email).IsUnique();
            b.Property(x => x.PasswordHash).IsRequired().HasMaxLength(200);
            b.HasOne(x => x.Role)
             .WithMany()
             .HasForeignKey("RoleId")
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);
            b.Property(x => x.IsActive).IsRequired();
            b.Property(x => x.CreatedAt).IsRequired();
            b.Property(x => x.LastLoginAt).IsRequired(false);
        });

        modelBuilder.Entity<Chief>(b =>
        {
            b.ToTable("Chiefs");
            b.HasMany(c => c.Employees)
             .WithOne()
             .HasForeignKey("ChiefId")
             .IsRequired(false)
             .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<TaskType>(b =>
        {
            b.ToTable("TaskTypes");
            b.HasKey(x => x.Id);
            b.Property(x => x.Name).IsRequired().HasMaxLength(100);
            b.HasIndex(x => x.Name).IsUnique();
        });
    }
}
