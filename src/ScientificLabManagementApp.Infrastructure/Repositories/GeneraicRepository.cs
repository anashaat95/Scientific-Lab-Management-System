using Microsoft.AspNetCore.Http;


namespace ScientificLabManagementApp.Infrastructure;

public class GenericRepository<TEntity, TDto> : IGenericRepository<TEntity, TDto>
    where TEntity : class, IEntityBase
    where TDto : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly IMapper _mapper;

    public GenericRepository()
    {
        var serviceProvider = new HttpContextAccessor().HttpContext!.RequestServices;
        _context = serviceProvider!.GetRequiredService<ApplicationDbContext>(); 
        _mapper = serviceProvider!.GetRequiredService<IMapper>();
    }

    public virtual async Task<TDto> GetDtoByIdAsync(string id)
    {
        Console.WriteLine(id);
        var result = await _context.Set<TEntity>()
                                   .Where(x => x.Id == id)
                                   .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                                   .FirstOrDefaultAsync();
        return result;
    }

    public virtual async Task<TEntity> GetEntityByIdAsync(string id)
    {
        var result = await _context.Set<TEntity>()
                                   .Where(x => x.Id == id)
                                   .FirstOrDefaultAsync();
        return result;
    }


    public virtual async Task<IEnumerable<TDto>> GetAllAsync()
    {
        return await _context.Set<TEntity>()
                             .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                             .ToListAsync();
    }

    public virtual async Task<IEnumerable<TDto>> FindAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var filtered = await _context.Set<TEntity>()
                              .Where(predicate)
                              .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                              .ToListAsync();
        return filtered;
    }

    public virtual async Task<TDto> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var result = await _context.Set<TEntity>()
                              .Where(predicate)
                              .ProjectTo<TDto>(_mapper.ConfigurationProvider)
                              .FirstOrDefaultAsync();
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

    public virtual Task DeleteAsync(TEntity entity)
    {
        _context.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual  Task SaveChangesAsync()
    { 
        return _context.SaveChangesAsync();
    }

    public async Task<bool> RelatedExistsAsync<RelatedEntity>(string id) where RelatedEntity : class, IEntityBase
    {
        return await _context.Set<RelatedEntity>().AnyAsync(e => e.Id == id);
    }

    public virtual async Task<RelatedEntity> FindRelatedEntityByIdAsync<RelatedEntity>(Expression<Func<RelatedEntity, bool>> predicate) where RelatedEntity : class, IEntityBase
    {
        var filtered = await _context.Set<RelatedEntity>()
                              .Where(predicate)
                              .FirstOrDefaultAsync();
        return filtered;
    }

}
