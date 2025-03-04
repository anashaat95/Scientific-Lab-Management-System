namespace ScientificLabManagementApp.Infrastructure;

public class GenericRepository<TEntity> : IGenericRepository<TEntity>
    where TEntity : class, IEntityBase
{
    protected readonly ApplicationDbContext _context;
    protected readonly IPropertyMappingService<TEntity, TEntity> _propertyMappingService;

    public GenericRepository(ApplicationDbContext context, IPropertyMappingService<TEntity, TEntity> propertyMappingService)
    {
        _context = context;
        _propertyMappingService = propertyMappingService;
    }

    public virtual async Task<TEntity> GetOneByIdAsync(string id, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await FindOneAsync(e => e.Id == id, includes);
        return result;
    }

    public DbSet<TEntity> GetEntitySet()
    {
        return _context.Set<TEntity>();
    }

    public async Task<PagedList<TEntity>> GetAllAsync(AllResourceParameters parameters, params Expression<Func<TEntity, object>>[] includes)
    {
        IQueryable<TEntity> query = _context.Set<TEntity>().ApplyIncludes(includes).AsQueryable();

        query = query.ApplyFilter(parameters.Filter);

        if (!_propertyMappingService.ValidateMappingExistsFor(parameters.OrderBy))
            throw new ArgumentException($"Key mapping for {parameters.OrderBy} is missing");

        query = query
                     .ApplySort<TEntity, TEntity>(parameters.OrderBy, _propertyMappingService.GetPropertyMapping());


        return await PagedList<TEntity>.CreateAsync(query, parameters.PageNumber, parameters.PageSize);
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

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _context.Set<TEntity>().AnyAsync(predicate);
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

    private IQueryable<TEntity> ApplyFilter(IQueryable<TEntity> query, string Filter)
    {
        var filterParts = Filter.Split(":");
        if (filterParts.Length != 2) return query;

        var propertyName = filterParts[0];
        var filterValue = filterParts[1];

        var Parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = Expression.Property(Parameter, propertyName);
        var constant = Expression.Constant(filterValue);
        var equals = Expression.Equal(property, constant);
        var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, Parameter);

        return query.Where(lambda);
    }

    public IQueryable<TEntity> ApplyFilteringSortingAndPagination(IQueryable<TEntity> query, AllResourceParameters parameters)
    {

        if (!String.IsNullOrEmpty(parameters.Filter)) query = ApplyFilter(query, parameters.Filter);

        return query;
    }
}
