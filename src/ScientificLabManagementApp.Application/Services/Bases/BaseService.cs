namespace ScientificLabManagementApp.Application;

public class BaseService<TEntity, TDto> : IBaseService<TEntity, TDto>
    where TEntity : class, IEntityBase
    where TDto : class, IEntityHaveId
{
    protected readonly IGenericRepository<TEntity, TDto> _repository;

    public BaseService()
    {
        var serviceProvider = new HttpContextAccessor().HttpContext!.RequestServices;
        _repository = serviceProvider!.GetRequiredService<IGenericRepository<TEntity, TDto>>();
    }

    public async Task<TDto> GetDtoByIdAsync(string id)
    {
        var result = await _repository.GetDtoByIdAsync(id);
        return result;
    }

    public async Task<TEntity> GetEntityByIdAsync(string id)
    {
        return await _repository.GetEntityByIdAsync(id);
    }


    public async Task<IEnumerable<TDto>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
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

    public async Task<RelatedEntity> FindRelatedEntityByIdAsync<RelatedEntity>(Expression<Func<RelatedEntity, bool>> predicate) where RelatedEntity : class, IEntityBase
    {
        return await _repository.FindRelatedEntityByIdAsync<RelatedEntity>(predicate);
    }

    public async Task<TDto> FindOneAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var result = await _repository.FindOneAsync(predicate);
        return result;
    }
}
