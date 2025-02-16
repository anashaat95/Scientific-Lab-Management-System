namespace ScientificLabManagementApp.Application;

public class DeleteCommandHandlerBase<TRequest, TEntity, TDto> : RequestHandlerBase<TRequest, TEntity, TDto>
    where TRequest : DeleteCommandBase<TDto>
    where TEntity : class, IEntityBase
    where TDto : class, IEntityHaveId
{
    public override async Task<Response<TDto>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var currentUser = _currentUserService.User;
        if (currentUser == null)
            return Unauthorized<TDto>("You are unauthorized to delete this resource.");

        var entityToDelete = await _basicService.GetEntityByIdAsync(request.Id);
        if (entityToDelete is null)
            return NotFound<TDto>($"No resource found with the id = {request.Id}");

        if (_currentUserService.UserRoles.Contains(enUserRoles.Admin.ToString()))
        {
            return await DoDelete(entityToDelete);
        }

        if (entityToDelete is IEntityAddedByUser entityAddedByUser)
        {
            var UserId = _currentUserService.UserId;

            if (entityAddedByUser.UserId.Equals(_currentUserService.UserId, StringComparison.OrdinalIgnoreCase))
                return await DoDelete(entityToDelete);
        }

        return Unauthorized<TDto>("You are unauthorized to delete this resource.");
    }

    protected virtual async Task<Response<TDto>> DoDelete(TEntity entityToDelete)
    {
        await _basicService.DeleteAsync(entityToDelete);
        return Deleted<TDto>();
    }
}
