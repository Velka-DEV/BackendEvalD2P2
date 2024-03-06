using BackendEvalD2P2.Application.Common.Interfaces.Repositories;
using BackendEvalD2P2.Domain.Entities;
using BackendEvalD2P2.Domain.Exceptions;
using MediatR;

namespace BackendEvalD2P2.Application.Events.Commands.DeleteEvent;

public class DeleteEventHandler : IRequestHandler<DeleteEventCommand>
{
    private readonly IEventRepository _eventRepository;

    public DeleteEventHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task Handle(DeleteEventCommand request, CancellationToken cancellationToken)
    {
        var @event = await _eventRepository.GetByIdAsync(request.Id, cancellationToken);

        if (@event is null)
        {
            throw new NotFoundException(nameof(Event), request.Id);
        }

        await _eventRepository.DeleteAsync(@event, cancellationToken);
    }
}