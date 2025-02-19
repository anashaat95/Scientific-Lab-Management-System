using System;

namespace ScientificLabManagementApp.Infrastructure;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class, IEntityBase
{
    protected readonly ApplicationDbContext _context;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public virtual async Task<TEntity> GetOneByIdAsync(string id, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await FindOneAsync(e=>e.Id == id, includes);
        return result;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _context.Set<TEntity>()
                           .ApplyIncludes(includes)
                           .ToListAsync();
        return result;
    }

    public IQueryable<TEntity> GetQueryableEntityAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return _context.Set<TEntity>().Where(predicate);
    }


    public virtual async Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _context.Set<TEntity>()
                                   .ApplyIncludes(includes)
                                   .Where(predicate)
                                   .FirstOrDefaultAsync();
        return result;
    }

    public virtual async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _context.Set<TEntity>()
                                   .ApplyIncludes(includes)
                                   .Where(predicate)
                                   .ToListAsync();
        return result;
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _context.Set<TEntity>().AnyAsync(e => e.Id == id);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        var addedEntity = await _context.AddAsync(entity);

        return addedEntity.Entity;
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity)
    {
        var updatedEntity = _context.Update(entity);
        return Task.FromResult(updatedEntity.Entity);
    }

    public virtual async Task<string> UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        _context.UpdateRange(entities);
        return await Task.FromResult("All entities are updated");
    }

    public virtual Task DeleteAsync(TEntity entity)
    {
        _context.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual Task SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    public async Task<bool> RelatedExistsAsync<RelatedEntity>(string id) where RelatedEntity : class, IEntityBase
    {
        return await _context.Set<RelatedEntity>().AnyAsync(e => e.Id == id);
    }

    public virtual async Task<RelatedEntity> FindRelatedEntityByIdAsync<RelatedEntity>(Expression<Func<RelatedEntity, bool>> predicate, params Expression<Func<RelatedEntity, object>>[] includes) where RelatedEntity : class, IEntityBase
    {
        var filtered = await _context.Set<RelatedEntity>()
                                     .ApplyIncludes(includes)
                                     .Where(predicate)
                                     .FirstOrDefaultAsync();
        return filtered;
    }
}
