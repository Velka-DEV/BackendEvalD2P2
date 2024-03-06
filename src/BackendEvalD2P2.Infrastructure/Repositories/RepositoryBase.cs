using BackendEvalD2P2.Application.Common.Interfaces.Repositories;
using BackendEvalD2P2.Domain.Entities.Common;
using BackendEvalD2P2.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendEvalD2P2.Infrastructure.Repositories;

public class RepositoryBase<TEntity>: IRepository<TEntity> where TEntity : EntityBase
{
    protected readonly ApplicationDbContext Context;

    public RepositoryBase(ApplicationDbContext context)
    {
        Context = context;
    }
    
    public async Task<TEntity?> GetByIdAsync(Guid id, CancellationToken token = default)
    {
        return await Context.Set<TEntity>().FindAsync([id], token);
    }
    
    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken token = default)
    {
        return await Context.Set<TEntity>().ToListAsync(token);
    }
    
    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken token = default)
    {
        await Context.Set<TEntity>().AddAsync(entity, token);
        await Context.SaveChangesAsync(token);
        return entity;
    }
    
    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token = default)
    {
        Context.Set<TEntity>().Update(entity);
        await Context.SaveChangesAsync(token);
        return entity;
    }

    public async Task<bool> DeleteAsync(TEntity entity, CancellationToken token = default)
    {
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync(token);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token = default)
    {
        var entity = await GetByIdAsync(id, token);
        if (entity == null)
        {
            return false;
        }
        
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync(token);
        return true;
    }
    
    public async Task<bool> ExistsAsync(Guid id, CancellationToken token = default)
    {
        return await GetByIdAsync(id, token) != null;
    }
    
    public async Task<int> CountAsync(CancellationToken token = default)
    {
        return await Context.Set<TEntity>().CountAsync(token);
    }
}