using BackendEvalD2P2.Application.Common.Interfaces.Repositories;
using BackendEvalD2P2.Application.Common.Mappings;
using BackendEvalD2P2.Application.Common.Models.DTOs;
using MediatR;

namespace BackendEvalD2P2.Application.Events.Commands.CreateEvent;

public class CreateEventHandler : IRequestHandler<CreateEventCommand, EventDto>
{
    private readonly IEventRepository _eventRepository;
    
    private readonly EventMapper _eventMapper = new();

    public CreateEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var @event = _eventMapper.Map(request);
        
        await _eventRepository.AddAsync(@event, cancellationToken);
        
        return _eventMapper.Map(@event);
    }
}