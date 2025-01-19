namespace ScientificLabManagementApp.Domain;
public interface IGenericRepository<TEntity, TDto>
    where TEntity : class, IEntityBase
    where TDto : class
{
    Task<TDto> GetDtoByIdAsync(string id);
    Task<TEntity> GetEntityByIdAsync(string id);
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<IEnumerable<TDto>> FindAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TDto> FindOneAsync(Expression<Func<TEntity, bool>> predicate);
    Task<bool> ExistsAsync(string id);
    Task<TDto> AddAsync(TEntity entity);
    Task<TDto> UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
    Task SaveChangesAsync();
    
    Task<bool> RelatedExistsAsync<RelatedEntity>(string id) where RelatedEntity : class, IEntityBase;
    Task<RelatedEntity> FindRelatedEntityByIdAsync<RelatedEntity>(Expression<Func<RelatedEntity, bool>> predicate) where RelatedEntity : class, IEntityBase;
}
