using BackendEvalD2P2.Application.Common.Models.DTOs;
using MediatR;

namespace BackendEvalD2P2.Application.Events.Queries.ListEvents;

public record ListEventsQuery : IRequest<IEnumerable<EventDto>>
{
    
}