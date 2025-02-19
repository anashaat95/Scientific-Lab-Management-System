namespace ScientificLabManagementApp.Application;

public class UpdateCommandHandlerBase<TRequest, TEntity, TDto> : RequestHandlerBase<TRequest, TEntity, TDto>
    where TRequest : IRequest<Response<TDto>>, IEntityHaveId
    where TEntity : class, IEntityBase
    where TDto : class, IEntityHaveId
{

    public override async Task<Response<TDto>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _basicService.GetEntityByIdAsync(request.Id);
        if (entityToUpdate is null)
            return NotFound<TDto>($"No resource found with the id = {request.Id}");

        if (!_basicService.IsAuthorizedToUpdateOrDeleteResource(entityToUpdate))
            return Unauthorized<TDto>("You are not authorized to update this resource.");

        var result = await DoUpdate(request, entityToUpdate);

        return result;
    }

    protected virtual async Task<Response<TDto>> DoUpdate(TRequest updateRequest, TEntity entityToUpdate)
    {
        var updatedEntity = _mapper.Map(updateRequest, entityToUpdate);
        await _basicService.UpdateAsync(updatedEntity);

        return Created(_mapper.Map<TDto>(entityToUpdate));
    }
}