using BackendEvalD2P2.Domain.Entities.Common;

namespace BackendEvalD2P2.Application.Common.Interfaces.Repositories;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token = default);
    
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token = default);
    
    Task<TEntity> AddAsync(TEntity entity, CancellationToken token = default);
    
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default);
    
    Task<bool> DeleteAsync(Guid id, CancellationToken token = default);
    
    Task<bool> ExistsAsync(Guid id, CancellationToken token = default);
    
    Task<int> CountAsync(CancellationToken token = default);
}