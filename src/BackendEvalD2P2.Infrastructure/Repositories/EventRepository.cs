using BackendEvalD2P2.Application.Common.Interfaces.Repositories;
using BackendEvalD2P2.Domain.Entities;
using BackendEvalD2P2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendEvalD2P2.Infrastructure.Repositories;

public class EventRepository : RepositoryBase<Event>, IEventRepository
{
    public EventRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Event>> GetEventsByDateAsync(DateTime date, CancellationToken cancellationToken)
    {
        return await Context.Set<Event>().Where(e => e.StartDate <= date && e.EndDate >= date).ToListAsync(cancellationToken);
    }
}