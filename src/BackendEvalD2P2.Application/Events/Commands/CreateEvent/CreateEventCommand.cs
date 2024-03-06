using BackendEvalD2P2.Application.Common.Models.DTOs;
using MediatR;

namespace BackendEvalD2P2.Application.Events.Commands.CreateEvent;

public record CreateEventCommand : IRequest<EventDto>
{
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public string Location { get; set; } = string.Empty;
}