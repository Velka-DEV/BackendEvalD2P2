using BackendEvalD2P2.Application.Common.Interfaces.Repositories;
using BackendEvalD2P2.Application.Common.Mappings;
using BackendEvalD2P2.Application.Common.Models.DTOs;
using BackendEvalD2P2.Domain.Entities;
using BackendEvalD2P2.Domain.Exceptions;
using MediatR;

namespace BackendEvalD2P2.Application.Events.Commands.EditEvent;

public class EditEventHandler : IRequestHandler<EditEventCommand, EventDto>
{
    private readonly IEventRepository _eventRepository;
    
    private readonly EventMapper _eventMapper = new();

    public EditEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<EventDto> Handle(EditEventCommand request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (@event is null)
        {
            throw new NotFoundException(nameof(Event), request.Id);
        }
        
        _eventMapper.Map(request, @event);
        
        await _eventRepository.UpdateAsync(@event, cancellationToken);
        
        return _eventMapper.Map(@event);
    }
}