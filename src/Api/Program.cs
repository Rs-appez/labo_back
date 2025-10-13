using ParcBack.Application.Abstractions;
using ParcBack.Application.Zones;
using ParcBack.Application.Zones.CreateZone;
using ParcBack.Application.Zones.GetZoneById;
using ParcBack.Application.Zones.ListZones;
using ParcBack.Infrastructure.Persistence;
using ParcBack.Infrastructure.Repositories;
using ParcBack.Domain.Abstractions;
using ParcBack.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using ParcBack.Application.Zones.DeleteZone;


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
        builder.Services.AddScoped<IZoneRepository, ZoneRepository>();

        // Handlers (CQS without MediatR)
        builder.Services.AddScoped<ICommandHandler<CreateZoneCommand, int>, CreateZoneHandler>();
        builder.Services.AddScoped<IQueryHandler<GetZoneByIdQuery, ZoneDto?>, GetZoneByIdHandler>();
        builder.Services.AddScoped<IQueryHandler<ListZonesQuery, IReadOnlyList<ZoneDto>>, ListZonesHandler>();
        builder.Services.AddScoped<ICommandHandler<DeleteZoneCommand, int>, DeleteZoneHandler>();


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
