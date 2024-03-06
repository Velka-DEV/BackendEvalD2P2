using BackendEvalD2P2.Application.Common.Models.DTOs;
using BackendEvalD2P2.Application.Events.Commands.CreateEvent;
using BackendEvalD2P2.Application.Events.Commands.EditEvent;
using BackendEvalD2P2.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace BackendEvalD2P2.Application.Common.Mappings;

[Mapper]
public partial class EventMapper
{
    [MapperIgnoreTarget(nameof(Event.Id))]
    public partial Event Map(CreateEventCommand source);
    
    [MapperIgnoreTarget(nameof(Event.Id))]
    public partial void Map(EditEventCommand source, Event target);
    
    public partial EventDto Map(Event source);
}