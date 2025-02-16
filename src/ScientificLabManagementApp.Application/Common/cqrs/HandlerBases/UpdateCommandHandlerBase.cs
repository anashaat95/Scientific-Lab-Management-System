namespace ScientificLabManagementApp.Application;

public class UpdateCommandHandlerBase<TRequest, TEntity, TDto> : RequestHandlerBase<TRequest, TEntity, TDto>
    where TRequest : IRequest<Response<TDto>>, IEntityHaveId
    where TEntity : class, IEntityBase
    where TDto : class, IEntityHaveId
{

    public override async Task<Response<TDto>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _basicService.GetEntityByIdAsync(request.Id);
        if (entityToUpdate is null) return NotFound<TDto>($"No resource found with the id = {request.Id}");


        if (_currentUserService.UserRoles.Contains(enUserRoles.Admin.ToString()))
        {
            return await DoUpdate(request, entityToUpdate);
        }


        if (entityToUpdate is IEntityAddedByUser entityAddedByUser)
        {
            var UserId = _currentUserService.UserId;

            if (entityAddedByUser.UserId.Equals(_currentUserService.UserId, StringComparison.OrdinalIgnoreCase))
                return await DoUpdate(request, entityToUpdate);
        }

        return Unauthorized<TDto>("You are unauthorized to delete this resource.");
    }

    protected virtual async Task<Response<TDto>> DoUpdate(TRequest updateRequest, TEntity entityToUpdate)
    {
        var updatedEntity = _mapper.Map(updateRequest, entityToUpdate);
        await _basicService.UpdateAsync(updatedEntity);

        return Created(_mapper.Map<TDto>(entityToUpdate));
    }
}