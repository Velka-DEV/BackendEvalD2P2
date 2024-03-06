using BackendEvalD2P2.Domain.Entities.Common;

namespace BackendEvalD2P2.Domain.Entities;

public class Event : EntityBase
{
    public string Title { get; set; } = string.Empty;
    
    public string Description { get; set; } = string.Empty;
    
    public DateTime StartDate { get; set; }
    
    public DateTime EndDate { get; set; }
    
    public string Location { get; set; } = string.Empty;
}