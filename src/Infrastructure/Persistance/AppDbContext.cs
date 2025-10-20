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

    public DbSet<EmployeeTask> Tasks => Set<EmployeeTask>();
    public DbSet<TaskType> TaskTypes => Set<TaskType>();
    public DbSet<Comment> Comments => Set<Comment>();

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
            b.HasMany(e => e.Tasks)
             .WithOne()
             .HasForeignKey("EmployeeId")
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
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

        modelBuilder.Entity<TaskType>().HasData(
            new TaskType { Id = 1, Name = "Site Maintenance" },
            new TaskType { Id = 2, Name = "Maintenance" },
            new TaskType { Id = 3, Name = "Repair" },
            new TaskType { Id = 4, Name = "Visitor reception" }
        );
        modelBuilder.Entity<EmployeeTask>(b =>
        {
            b.ToTable("Tasks");
            b.HasKey(x => x.Id);
            b.HasOne(x => x.Type)
             .WithMany()
             .HasForeignKey("TypeId")
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);
            b.Property(x => x.IsCompleted).IsRequired();
            b.Property(x => x.IsValidated).IsRequired();
            b.HasOne(x => x.EmployeeAssigned)
             .WithMany(e => e.Tasks)
             .HasForeignKey("EmployeeId")
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Comment>(b =>
        {
            b.ToTable("Comments");
            b.HasKey(x => x.Id);
            b.Property(x => x.Content).IsRequired().HasMaxLength(1000);
            b.Property(x => x.CreatedAt).IsRequired();
            b.HasOne(x => x.Task)
             .WithMany()
             .HasForeignKey("TaskId")
             .IsRequired()
             .OnDelete(DeleteBehavior.Cascade);
            b.HasOne(x => x.Employee)
             .WithMany()
             .HasForeignKey("EmployeeId")
             .IsRequired()
             .OnDelete(DeleteBehavior.SetNull);
        });
    }

}
