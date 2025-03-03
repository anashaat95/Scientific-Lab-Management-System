using Microsoft.EntityFrameworkCore;

namespace ScientificLabManagementApp.Domain;
public interface IGenericRepository<TEntity>
    where TEntity : class, IEntityBase
{
    DbSet<TEntity> GetEntitySet();
    Task<TEntity> GetOneByIdAsync(string id, params Expression<Func<TEntity, object>>[] includes);
    Task<PagedList<TEntity>> GetAllAsync(AllResourceParameters parameters, params Expression<Func<TEntity, object>>[] includes);
    Task<TEntity> FindOneAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes);
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<string> UpdateRangeAsync(IEnumerable<TEntity> entities);
    Task DeleteAsync(TEntity entity);
    Task SaveChangesAsync();

    Task<bool> RelatedExistsAsync<RelatedEntity>(string id) where RelatedEntity : class, IEntityBase;
    Task<RelatedEntity> FindRelatedEntityByIdAsync<RelatedEntity>(Expression<Func<RelatedEntity, bool>> predicate, params Expression<Func<RelatedEntity, object>>[] includes) where RelatedEntity : class, IEntityBase;

    IQueryable<TEntity> GetQueryableEntityAsync(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> ApplyFilteringSortingAndPagination(IQueryable<TEntity> query, AllResourceParameters parameters);
}
