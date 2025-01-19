namespace ScientificLabManagementApp.Application;

public class UpdateCommandHandlerBase<TRequest, TEntity, TDto> : RequestHandlerBase<TRequest, TEntity, TDto>
    where TRequest : IRequest<Response<TDto>>, IEntityHaveId
    where TEntity : class
    where TDto : class
{

    public override async Task<Response<TDto>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _basicService.GetEntityByIdAsync(request.Id);
        if (entityToUpdate is null) return NotFound<TDto>($"No resource found with the id = {request.Id}");


        if (_currentUserService.UserRoles.Contains(enUserRoles.Admin.ToString()))
        {
            await DoUpdate(request, entityToUpdate);
        }
        else if (entityToUpdate is IEntityAddedByUser entityAddedByUser)
        {
            var UserId = _currentUserService.UserId;

            if (entityAddedByUser.UserId.Equals(_currentUserService.UserId, StringComparison.OrdinalIgnoreCase))
                await DoUpdate(request, entityToUpdate);
            else
                return Unauthorized<TDto>("You are unauthorized to delete this resource.");
        }

        return Deleted<TDto>();
    }

    protected async Task DoUpdate(TRequest updateRequest, TEntity entityToUpdate)
    {
        await _basicService.UpdateAsync(_mapper.Map(updateRequest, entityToUpdate));
    }
}