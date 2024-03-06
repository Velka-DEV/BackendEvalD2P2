using BackendEvalD2P2.Application.Common.Interfaces.Repositories;
using BackendEvalD2P2.Application.Common.Mappings;
using BackendEvalD2P2.Application.Common.Models.DTOs;
using MediatR;

namespace BackendEvalD2P2.Application.Events.Queries.ListEvents;

public class ListEventsHandler : IRequestHandler<ListEventsQuery, IEnumerable<EventDto>>
{
    private readonly IEventRepository _eventRepository;
    private readonly EventMapper _eventMapper = new();

    public ListEventsHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }


    public async Task<IEnumerable<EventDto>> Handle(ListEventsQuery request, CancellationToken cancellationToken)
    {
        var events = request.Date.HasValue
            ? await _eventRepository.GetEventsByDateAsync(request.Date.Value, cancellationToken)
            : await _eventRepository.GetAllAsync(cancellationToken);

        return events.Select(e => _eventMapper.Map(e));
    }
}