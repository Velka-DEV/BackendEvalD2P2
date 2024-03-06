using BackendEvalD2P2.Application.Common.Interfaces.Repositories;
using BackendEvalD2P2.Domain.Entities;
using BackendEvalD2P2.Infrastructure.Data;

namespace BackendEvalD2P2.Infrastructure.Repositories;

public class EventRepository : RepositoryBase<Event>, IEventRepository
{
    public EventRepository(ApplicationDbContext context) : base(context)
    {
    }
}