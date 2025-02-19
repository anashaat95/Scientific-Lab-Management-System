namespace ScientificLabManagementApp.Application;

public class DeleteCommandHandlerBase<TRequest, TEntity, TDto> : RequestHandlerBase<TRequest, TEntity, TDto>
    where TRequest : DeleteCommandBase<TDto>
    where TEntity : class, IEntityBase
    where TDto : class, IEntityHaveId
{
    public override async Task<Response<TDto>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var entityToDelete = await _basicService.GetEntityByIdAsync(request.Id);
        if (entityToDelete is null)
            return NotFound<TDto>($"No resource found with the id = {request.Id}");
        if (!_basicService.IsAuthorizedToUpdateOrDeleteResource(entityToDelete))
            return Unauthorized<TDto>("You are not authorized to update this resource.");

        return await DoDelete(entityToDelete);
    }

    protected virtual async Task<Response<TDto>> DoDelete(TEntity entityToDelete)
    {
        await _basicService.DeleteAsync(entityToDelete);
        return Deleted<TDto>();
    }
}
