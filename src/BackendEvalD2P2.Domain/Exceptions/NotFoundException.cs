namespace BackendEvalD2P2.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string name, object key)
        : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
    
    public NotFoundException(Type type, object key)
        : base($"Entity \"{type.Name}\" ({key}) was not found.")
    {
    }
    
    public NotFoundException(Type type, object key, Exception innerException)
        : base($"Entity \"{type.Name}\" ({key}) was not found.", innerException)
    {
    }
}