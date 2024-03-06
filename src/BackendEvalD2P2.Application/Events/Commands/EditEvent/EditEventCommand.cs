using System.Text.Json.Serialization;
using BackendEvalD2P2.Application.Common.Models.DTOs;
using MediatR;

namespace BackendEvalD2P2.Application.Events.Commands.EditEvent;

public record EditEventCommand : IRequest<EventDto>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public string Location { get; set; } = string.Empty;
}