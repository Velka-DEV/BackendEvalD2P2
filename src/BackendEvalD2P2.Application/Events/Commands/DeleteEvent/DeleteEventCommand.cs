using MediatR;

namespace BackendEvalD2P2.Application.Events.Commands.DeleteEvent;

public record DeleteEventCommand : IRequest
{
    public Guid Id { get; set; }
}