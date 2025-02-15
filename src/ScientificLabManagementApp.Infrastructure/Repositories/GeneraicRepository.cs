namespace ScientificLabManagementApp.Infrastructure;

public class GenericRepository<TEntity, TDto> : IGenericRepository<TEntity, TDto>
    where TEntity : class, IEntityBase
    where TDto : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly IMapper _mapper;

    public GenericRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public virtual async Task<TDto> GetDtoByIdAsync(string id, params Expression<Func<TEntity, object>>[] includes)
    {

        var result = await _context.Set<TEntity>()
                               .ApplyIncludes(includes)
                               .Where(x => x.Id == id).AsNoTracking()
                               .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                               .FirstOrDefaultAsync();

        return result;
    }

    public virtual async Task<TEntity> GetEntityByIdAsync(string id, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _context.Set<TEntity>()
                                   .ApplyIncludes(includes)
                                   .Where(x => x.Id == id)
                                   .FirstOrDefaultAsync();
        return result;
    }


    public virtual async Task<IEnumerable<TDto>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        return await _context.Set<TEntity>()
                             .ApplyIncludes(includes).AsNoTracking()
                             .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                             .ToListAsync();
    }

    public virtual async Task<IEnumerable<TDto>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var filtered = await _context.Set<TEntity>()
                                     .ApplyIncludes(includes)
                                     .Where(predicate)
                                     .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                                     .ToListAsync();
        return filtered;
    }



    public virtual async Task<TDto> FindOneAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _context.Set<TEntity>()
                                   .ApplyIncludes(includes)
                                   .Where(predicate)
                                   .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                                   .FirstOrDefaultAsync();
        return result;
    }

    public virtual async Task<TEntity> FindOneEntityAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _context.Set<TEntity>()
                                   .ApplyIncludes(includes)
                                   .Where(predicate)
                                   .FirstOrDefaultAsync();
        return result;
    }

    public virtual async Task<IEnumerable<TDto>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _context.Set<TEntity>()
                                   .ApplyIncludes(includes)
                                   .Where(predicate)
                                   .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                                   .ToListAsync();
        return result;
    }

    public virtual async Task<IEnumerable<TEntity>> FindEntitiesAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
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

    public virtual async Task<TDto> AddAsync(TEntity entity)
    {
        var addedEntity = await _context.AddAsync(entity);

        return _mapper.Map<TDto>(addedEntity.Entity);
    }

    public virtual Task<TDto> UpdateAsync(TEntity entity)
    {
        var updatedEntity = _context.Update(entity);
        return Task.FromResult(_mapper.Map<TDto>(updatedEntity.Entity));
    }

    public virtual async Task<string> UpdateAllAsync(IEnumerable<TEntity> entities)
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
