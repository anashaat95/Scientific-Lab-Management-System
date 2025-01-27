using System.Linq.Expressions;

namespace ScientificLabManagementApp.Application;

public interface IBaseService<TEntity, TDto>
    where TEntity : class, IEntityBase
    where TDto : class, IEntityHaveId
{
    Task<TDto> GetDtoByIdAsync(string id, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> GetEntityByIdAsync(string id, params Expression<Func<TEntity, object>>[] includes);
    Task<IEnumerable<TDto>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes);
    Task<TDto> AddAsync(TEntity entityToAdd);
    Task<TDto> UpdateAsync(TEntity entityToUpdate);
    Task<bool> ExistsAsync(string id);
    Task DeleteAsync(TEntity entity);
    Task<bool> RelatedExistsAsync<RelatedEntity>(string id) where RelatedEntity : class, IEntityBase;
    Task<RelatedEntity> FindRelatedEntityByIdAsync<RelatedEntity>(Expression<Func<RelatedEntity, bool>> predicate, params Expression<Func<RelatedEntity, object>>[] includes) where RelatedEntity : class, IEntityBase;

    Task<TDto> FindOneAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
}
