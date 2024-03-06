using System.Text.Json.Serialization;
using BackendEvalD2P2.Application.Common.Models.DTOs;
using MediatR;

namespace BackendEvalD2P2.Application.Events.Queries.GetEvent;

public class GetEventQuery : IRequest<EventDto>
{
    [JsonIgnore]
    public Guid Id { get; set; }
}