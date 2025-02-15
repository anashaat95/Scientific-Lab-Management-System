namespace ScientificLabManagementApp.Domain;
public interface IGenericRepository<TEntity, TDto>
    where TEntity : class, IEntityBase
    where TDto : class
{
    Task<TDto> GetDtoByIdAsync(string id, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> GetEntityByIdAsync(string id, params Expression<Func<TEntity, object>>[] includes);
    Task<IEnumerable<TDto>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
    Task<IEnumerable<TDto>> FindAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    Task<TDto> FindOneAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    Task<bool> ExistsAsync(string id);
    Task<TDto> AddAsync(TEntity entity);
    Task<TDto> UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task SaveChangesAsync();

    Task<bool> RelatedExistsAsync<RelatedEntity>(string id) where RelatedEntity : class, IEntityBase;
    Task<RelatedEntity> FindRelatedEntityByIdAsync<RelatedEntity>(Expression<Func<RelatedEntity, bool>> predicate, params Expression<Func<RelatedEntity, object>>[] includes) where RelatedEntity : class, IEntityBase;

    Task<string> UpdateAllAsync(IEnumerable<TEntity> entities);
    Task<IEnumerable<TEntity>> FindEntitiesAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    Task<IEnumerable<TDto>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> FindOneEntityAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
}
