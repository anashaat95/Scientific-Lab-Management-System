
namespace ScientificLabManagementApp.Application;
public class GetManyUserHandler : GetManyQueryHandlerBase<GetManyUserQuery, ApplicationUser, UserDto>
{
    protected override Task<IEnumerable<UserDto>> GetEntityDtos()
    {
        return _applicationUserService.GetAllAsync();
    }
}

public class GetManyTechnicianHandler : GetManyQueryHandlerBase<GetManyTechnicianQuery, ApplicationUser, UserDto>
{
    protected override Task<IEnumerable<UserDto>> GetEntityDtos()
    {
        return _applicationUserService.GetTechniciansAsync();
    }
}

public class GetManyLabSupervisorHandler : GetManyQueryHandlerBase<GetManySupervisiorQuery, ApplicationUser, UserDto>
{
    protected override Task<IEnumerable<UserDto>> GetEntityDtos()
    {
        return _applicationUserService.GetSupervisorsAsync();
    }
}

public class GetManyResearcherHandler : GetManyQueryHandlerBase<GetManySupervisiorQuery, ApplicationUser, UserDto>
{
    protected override Task<IEnumerable<UserDto>> GetEntityDtos()
    {
        return _applicationUserService.GetResearchersAsync();
    }
}

public class GetOneUserByIdHandler : GetOneQueryHandlerBase<GetOneUserByIdQuery, ApplicationUser, UserDto>
{
    protected override Task<UserDto?> GetEntityDto(GetOneUserByIdQuery request)
    {
        return _applicationUserService.GetDtoByIdAsync(request.Id);
    }
}


public class AddUserHandler : AddCommandHandlerBase<AddUserCommand, ApplicationUser, UserDto>
{

    public override async Task<Response<UserDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var entityToAdd = _mapper.Map<ApplicationUser>(request);
        var creationResult = await _userManager.CreateAsync(entityToAdd, request.Data.Password);
        if (!creationResult.Succeeded)
        {
            return BadRequest<UserDto>($"Failed to add the user. Errors: {creationResult.ConvertErrorsToString()}");
        }

        var roleAssignmentResult = await _userManager.AddToRoleAsync(entityToAdd, enUserRoles.User.ToString());
        if (!roleAssignmentResult.Succeeded)
        {
            return BadRequest<UserDto>($"User created but failed to assign role. Errors: {roleAssignmentResult.ConvertErrorsToString()}");
        }
        var updatedUserDto = await _applicationUserService.GetDtoByIdAsync(entityToAdd.Id);
        return Created<UserDto>(updatedUserDto);
    }
}

public class UpdateUserHandler : UpdateCommandHandlerBase<UpdateUserCommand, ApplicationUser, UserDto>
{

    public override async Task<Response<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _userManager.FindByIdAsync(request.Id);
        if (entityToUpdate is null) return NotFound<UserDto>($"No resource found with the id = {request.Id}");

        var mappedUserEntity = _mapper.Map(request, entityToUpdate);
        var updateResult = await _userManager.UpdateAsync(mappedUserEntity);

        if (!updateResult.Succeeded)
        {
            return BadRequest<UserDto>($"Failed to update the user. Errors: {updateResult.ConvertErrorsToString()}");
        }

        var updatedUserDto = await _applicationUserService.GetDtoByIdAsync(mappedUserEntity.Id);
        return Updated<UserDto>(updatedUserDto);
    }
}

public class DeleteUserHandler : DeleteCommandHandlerBase<DeleteUserCommand, ApplicationUser, UserDto>
{
    public override async Task<Response<UserDto>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entityToDelete = await _userManager.FindByIdAsync(request.Id);
        if (entityToDelete is null) return NotFound<UserDto>($"No resource found with the id = {request.Id}");

        var userRoles = await _userManager.GetRolesAsync(entityToDelete);

        if (userRoles.Contains(enUserRoles.Admin.ToString()))
        {
            return BadRequest<UserDto>($"Failed to delete the user. It is the admin. Cannot be deleted !");
        }

        var deleteResult = await _userManager.DeleteAsync(entityToDelete);
        if (!deleteResult.Succeeded)
        {
            return BadRequest<UserDto>($"Failed to delete the user. Errors: {deleteResult.ConvertErrorsToString()}");
        }

        return Deleted<UserDto>();
    }
}