using BackendEvalD2P2.Application.Common.Interfaces.Repositories;
using BackendEvalD2P2.Application.Common.Mappings;
using BackendEvalD2P2.Application.Common.Models.DTOs;
using BackendEvalD2P2.Domain.Entities;
using BackendEvalD2P2.Domain.Exceptions;
using MediatR;

namespace BackendEvalD2P2.Application.Events.Queries.GetEvent;

public class GetEventHandler : IRequestHandler<GetEventQuery, EventDto>
{
    private readonly IEventRepository _eventRepository;
    private readonly EventMapper _eventMapper = new();

    public GetEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventDto> Handle(GetEventQuery query, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(query.Id, cancellationToken);
        
        if (@event is null)
        {
            throw new NotFoundException(nameof(Event), query.Id);
        }

        return _eventMapper.Map(@event);
    }
}