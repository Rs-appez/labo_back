using ParcBack.Domain.Abstractions;
using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using MediatR;


namespace ParcBack.Application.Rides.CreateRide;

public class CreateRideHandler : IRequestHandler<CreateRideCommand, int>
{
    private readonly IRideRepository _repo;
    private readonly IZoneRepository _zoneRepo;
    private readonly IUnitOfWork _uow;

    public CreateRideHandler(IRideRepository repo, IZoneRepository zoneRepo, IUnitOfWork uow)
    {
        _repo = repo;
        _zoneRepo = zoneRepo;
        _uow = uow;
    }

    public async Task<int> Handle(CreateRideCommand command, CancellationToken ct)
    {
        Zone? zone = await _zoneRepo.GetByIdAsync(command.ZoneId, ct);
        if (zone == null)
        {
            throw new ArgumentException($"Zone with id {command.ZoneId} does not exist");
        }
        Ride item = new()
        {
            Name = command.Name,
            Zone = zone,
        };
        await _repo.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);

        return item.Id;
    }

}
