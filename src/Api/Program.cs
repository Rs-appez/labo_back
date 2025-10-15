using ParcBack.Application.Zones.CreateZone;
using ParcBack.Infrastructure.Persistence;
using ParcBack.Infrastructure.Repositories;
using ParcBack.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;


internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var conn = builder.Configuration.GetConnectionString("Default") ?? "Data Source=app.db";
        builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlite(conn));


        // Infra
        builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
        builder.Services.Scan(scan => scan
            .FromAssemblyOf<ZoneRepository>()
            .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        builder.Services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(CreateZoneHandler).Assembly);
        });


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.MapControllers();
        app.Run();
    }
}
