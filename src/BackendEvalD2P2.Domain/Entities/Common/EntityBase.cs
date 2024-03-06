namespace BackendEvalD2P2.Domain.Entities.Common;

public abstract class EntityBase
{
    public Guid Id { get; set; } = Guid.NewGuid();
}