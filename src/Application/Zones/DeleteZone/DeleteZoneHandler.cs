using ParcBack.Application.Abstractions;
using ParcBack.Domain.Abstractions;
using ParcBack.Domain.Entities;
using ParcBack.Domain.Repositories;

namespace ParcBack.Application.Zones.DeleteZone;

public class DeleteZoneHandler : ICommandHandler<DeleteZoneCommand, int>
{
    private readonly IZoneRepository _repo;
    private readonly IUnitOfWork _uow;

    public DeleteZoneHandler(IZoneRepository repo, IUnitOfWork uow)
    {
        _repo = repo;
        _uow = uow;
    }

    public async Task<int> Handle(DeleteZoneCommand command, CancellationToken ct)
    {
        var item = await _repo.GetByIdAsync(command.Id, ct);
        if (item is null)
        {
            throw new InvalidOperationException($"Zone with id {command.Id} not found");
        }
        _repo.Remove(item);
        await _uow.SaveChangesAsync(ct);
        return item.Id;
    }

}
