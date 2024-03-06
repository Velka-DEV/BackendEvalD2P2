using BackendEvalD2P2.Domain.Entities;

namespace BackendEvalD2P2.Application.Common.Interfaces.Repositories;

public interface IEventRepository : IRepository<Event>
{
    public Task<IEnumerable<Event>> GetEventsByDateAsync(DateTime date, CancellationToken cancellationToken);
}