
namespace ScientificLabManagementApp.Application;
public class GetManyUserHandler : GetManyQueryHandlerBase<GetManyUserQuery, ApplicationUser, UserDto>
{
    protected override Task<IEnumerable<UserDto>> GetEntityDtos()
    {
        return _applicationUserService.GetAllUsersByRoleAsync();
    }
}

public class GetManyUserSelectOptionsHandler : GetManySelectOptionsQueryHandler<GetManyUserSelectOptionsQuery, MappingApplicationUser>
{
    protected override Task<IEnumerable<SelectOptionDto>> GetEntityDtos()
    {
        return _applicationUserService.GetAllUsersSelectOptionsByRoleAsync();
    }
}

public class GetManyTechnicianHandler : GetManyQueryHandlerBase<GetManyTechnicianQuery, ApplicationUser, UserDto>
{
    protected override Task<IEnumerable<UserDto>> GetEntityDtos()
    {
        return _applicationUserService.GetAllUsersByRoleAsync(enUserRoles.Technician.ToString());
    }
}

public class GetManyTechnicianSelectOptionsHandler : GetManySelectOptionsQueryHandler<GetManyTechnicianSelectOptionsQuery, MappingApplicationUser>
{
    protected override Task<IEnumerable<SelectOptionDto>> GetEntityDtos()
    {
        return _applicationUserService.GetAllUsersSelectOptionsByRoleAsync(enUserRoles.Technician.ToString());
    }
}

public class GetManyLabSupervisorHandler : GetManyQueryHandlerBase<GetManySupervisorQuery, ApplicationUser, UserDto>
{
    protected override Task<IEnumerable<UserDto>> GetEntityDtos()
    {
        return _applicationUserService.GetAllUsersByRoleAsync(enUserRoles.LabSupervisor.ToString());
    }
}

public class GetManyLabSupervisorSelectOptionsHandler : GetManySelectOptionsQueryHandler<GetManySupervisorSelectOptionsQuery, MappingApplicationUser>
{
    protected override Task<IEnumerable<SelectOptionDto>> GetEntityDtos()
    {
        return _applicationUserService.GetAllUsersSelectOptionsByRoleAsync(enUserRoles.LabSupervisor.ToString());
    }
}

public class GetManyResearcherHandler : GetManyQueryHandlerBase<GetManySupervisorQuery, ApplicationUser, UserDto>
{
    protected override Task<IEnumerable<UserDto>> GetEntityDtos()
    {
        return _applicationUserService.GetAllUsersByRoleAsync(enUserRoles.Researcher.ToString());
    }
}

public class GetManyResearcherSelectOptionsHandler : GetManySelectOptionsQueryHandler<GetManyResearcherSelectOptionsQuery, MappingApplicationUser>
{
    protected override Task<IEnumerable<SelectOptionDto>> GetEntityDtos()
    {
        return _applicationUserService.GetAllUsersSelectOptionsByRoleAsync(enUserRoles.Researcher.ToString());
    }
}


public class GetOneUserByIdHandler : GetOneQueryHandlerBase<GetOneUserByIdQuery, ApplicationUser, UserDto>
{
    protected override Task<UserDto?> GetEntityDto(GetOneUserByIdQuery request)
    {
        return _applicationUserService.GetOneByIdAsync(request.Id);
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
        var updatedUserDto = await _applicationUserService.GetOneByIdAsync(entityToAdd.Id);
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

        var updatedUserDto = await _applicationUserService.GetOneByIdAsync(mappedUserEntity.Id);
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