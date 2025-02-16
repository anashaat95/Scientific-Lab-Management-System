namespace ScientificLabManagementApp.Application;

public class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto>
    where TEntity : class, IEntityBase
    where TDto : class, IEntityHaveId
{
    protected readonly IGenericRepository<TEntity> _repository;
    protected readonly IMapper _mapper;

    public BaseService(IGenericRepository<TEntity> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public virtual async Task<TDto> GetDtoByIdAsync(string id, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _repository.GetOneByIdAsync(id, includes);
        return _mapper.Map<TDto>(result);
    }

    public virtual async Task<TEntity> GetEntityByIdAsync(string id, params Expression<Func<TEntity, object>>[] includes)
    {
        return await _repository.GetOneByIdAsync(id, includes);
    }


    public virtual async Task<IEnumerable<TDto>> GetAllAsync(params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _repository.GetAllAsync(includes);
        return _mapper.Map<IEnumerable<TDto>>(result);
    }

    public virtual async Task<TDto> AddAsync(TEntity entityToAdd)
    {
        var resultDto = await _repository.AddAsync(entityToAdd);
        await _repository.SaveChangesAsync();

        return await GetDtoByIdAsync(resultDto.Id);
    }


    public virtual async Task<TDto> UpdateAsync(TEntity entityToUpdate)
    {
        var resultDto = await _repository.UpdateAsync(entityToUpdate);
        await _repository.SaveChangesAsync();
        return await GetDtoByIdAsync(resultDto.Id);
    }

    public virtual async Task<string> UpdateRangeAsync(IEnumerable<TEntity> entitiesToUpdate)
    {
        var result = await _repository.UpdateRangeAsync(entitiesToUpdate);
        await _repository.SaveChangesAsync();
        return await Task.FromResult(result);
    }

    public virtual async Task DeleteAsync(TEntity entity)
    {
        await _repository.DeleteAsync(entity);
        await _repository.SaveChangesAsync();
    }

    public virtual async Task<bool> ExistsAsync(string id)
    {
        return await _repository.ExistsAsync(id);
    }

    public virtual async Task<bool> RelatedExistsAsync<RelatedEntity>(string id) where RelatedEntity : class, IEntityBase
    {
        return await _repository.RelatedExistsAsync<RelatedEntity>(id);
    }

    public virtual async Task<RelatedEntity> FindRelatedEntityByIdAsync<RelatedEntity>(Expression<Func<RelatedEntity, bool>> predicate, params Expression<Func<RelatedEntity, object>>[] includes) where RelatedEntity : class, IEntityBase
    {
        return await _repository.FindRelatedEntityByIdAsync(predicate, includes);
    }

    public virtual async Task<TDto> FindOneAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _repository.FindOneAsync(predicate, includes);
        return _mapper.Map<TDto>(result);
    }

    public virtual async Task<IEnumerable<TDto>> FindAllAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _repository.FindAllAsync(predicate, includes);
        return _mapper.Map<IEnumerable<TDto>>(result);
    }

    public virtual async Task<TEntity> FindOneEntityAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _repository.FindOneAsync(predicate, includes);
        return result;
    }

    public virtual async Task<IEnumerable<TEntity>> FindEntitiesAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includes)
    {
        var result = await _repository.FindAllAsync(predicate, includes);
        return result;
    }
}
