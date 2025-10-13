using ParcBack.Domain.Abstractions;
using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;
using MediatR;


namespace ParcBack.Application.Zones.CreateZone;

public class CreateZoneHandler : IRequestHandler<CreateZoneCommand, int>
{
    private readonly IZoneRepository _repo;
    private readonly IUnitOfWork _uow;

    public CreateZoneHandler(IZoneRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<int> Handle(CreateZoneCommand command, CancellationToken ct)
    {
        if (await _repo.GetByThemeAsync(command.Theme, ct) is not null)
        {
            throw new InvalidOperationException($"Zone with name {command.Theme} already exists");
        }
        var item = new Zone(){
            Theme = command.Theme,
        };
        await _repo.AddAsync(item, ct);
        await _uow.SaveChangesAsync(ct);

        return item.Id;
    }

}
