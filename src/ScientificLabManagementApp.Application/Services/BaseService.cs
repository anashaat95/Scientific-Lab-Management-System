namespace ScientificLabManagementApp.Application;

public class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto>
    where TEntity : class, IEntityBase
    where TDto : class, IEntityHaveId
{
    protected readonly IGenericRepository<TEntity, TDto> _repository;

    public BaseService(IGenericRepository<TEntity, TDto> repository)
    {
        _repository = repository;
    }

    public async Task<TDto> GetDtoByIdAsync(string id, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _repository.GetDtoByIdAsync(id, includes);
        return result;
    }

    public async Task<TEntity> GetEntityByIdAsync(string id, params Expression<Func<TEntity, object>>[] includes)
    {
        return await _repository.GetEntityByIdAsync(id, includes);
    }


    public async Task<IEnumerable<TDto>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        return await _repository.GetAllAsync(includes);
    }

    public async Task<TDto> AddAsync(TEntity entityToAdd)
    {
        var resultDto = await _repository.AddAsync(entityToAdd);
        await _repository.SaveChangesAsync();

        return await _repository.GetDtoByIdAsync(resultDto.Id);
    }


    public async Task<TDto> UpdateAsync(TEntity entityToUpdate)
    {
        var resultDto = await _repository.UpdateAsync(entityToUpdate);
        await _repository.SaveChangesAsync();
        return await _repository.GetDtoByIdAsync(resultDto.Id);
    }

    public async Task<string> UpdateRangeAsync(IEnumerable<TEntity> entitiesToUpdate)
    {
        var result = await _repository.UpdateAllAsync(entitiesToUpdate);
        await _repository.SaveChangesAsync();
        return await Task.FromResult(result);
    }

    public async Task DeleteAsync(TEntity entity)
    {
        await _repository.DeleteAsync(entity);
        await _repository.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string id)
    {
        return await _repository.ExistsAsync(id);
    }

    public async Task<bool> RelatedExistsAsync<RelatedEntity>(string id) where RelatedEntity : class, IEntityBase
    {
        return await _repository.RelatedExistsAsync<RelatedEntity>(id);
    }

    public async Task<RelatedEntity> FindRelatedEntityByIdAsync<RelatedEntity>(Expression<Func<RelatedEntity, bool>> predicate, params Expression<Func<RelatedEntity, object>>[] includes) where RelatedEntity : class, IEntityBase
    {
        return await _repository.FindRelatedEntityByIdAsync(predicate, includes);
    }

    public async Task<TDto> FindOneAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _repository.FindOneAsync(predicate, includes);
        return result;
    }

    public async Task<IEnumerable<TDto>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _repository.FindAllAsync(predicate, includes);
        return result;
    }

    public async Task<TEntity> FindOneEntityAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _repository.FindOneEntityAsync(predicate, includes);
        return result;
    }

    public async Task<IEnumerable<TEntity>> FindEntitiesAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _repository.FindEntitiesAsync(predicate, includes);
        return result;
    }
}
