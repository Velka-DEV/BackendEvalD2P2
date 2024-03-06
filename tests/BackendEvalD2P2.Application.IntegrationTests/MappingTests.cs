using BackendEvalD2P2.Application.Common.Mappings;
using BackendEvalD2P2.Application.Events.Commands.CreateEvent;
using BackendEvalD2P2.Application.Events.Commands.EditEvent;
using BackendEvalD2P2.Domain.Entities;

namespace BackendEvalD2P2.Application.IntegrationTests;

public class MappingTests
{
    private readonly EventMapper _mapper = new();

    [Fact]
    public void ShouldMapEventToEventDto()
    {
        var @event = new Event
        {
            Id = Guid.NewGuid(),
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Location = "Test Location",
            Description = "Test Description"
        };
        
        var result = _mapper.Map(@event);
        
        Assert.Equal(@event.Id, result.Id);
        Assert.Equal(@event.StartDate, result.StartDate);
        Assert.Equal(@event.EndDate, result.EndDate);
        Assert.Equal(@event.Location, result.Location);
        Assert.Equal(@event.Description, result.Description);
    }
    
    [Fact]
    public void ShouldMapCreateEventCommandToEvent()
    {
        var createEventCommand = new CreateEventCommand
        {
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Location = "Test Location",
            Description = "Test Description"
        };
        
        var result = _mapper.Map(createEventCommand);
        
        Assert.Equal(createEventCommand.StartDate, result.StartDate);
        Assert.Equal(createEventCommand.EndDate, result.EndDate);
        Assert.Equal(createEventCommand.Location, result.Location);
        Assert.Equal(createEventCommand.Description, result.Description);
    }
    
    [Fact]
    public void ShouldMapEditEventCommandToEvent()
    {
        var @event = new Event
        {
            Id = Guid.NewGuid(),
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(2),
            Location = "Test Location 1",
            Description = "Test Description 1"
        };
        
        var editEventCommand = new EditEventCommand
        {
            Id = Guid.NewGuid(),
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(1),
            Location = "Test Location",
            Description = "Test Description"
        };
        
        _mapper.Map(editEventCommand, @event);
        
          Assert.Equal(editEventCommand.StartDate, @event.StartDate);
          Assert.Equal(editEventCommand.EndDate, @event.EndDate);
          Assert.Equal(editEventCommand.Location, @event.Location);
          Assert.Equal(editEventCommand.Description, @event.Description);
          Assert.NotEqual(editEventCommand.Id, @event.Id);
    }
}