namespace ScientificLabManagementApp.Application;
public class GetManyUserHandler : GetManyQueryHandlerBase<GetManyUserQuery, ApplicationUser, UserDto>
{
    public override async Task<Response<IEnumerable<UserDto>>> Handle(GetManyUserQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users
                                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                                .ToListAsync(); 
        
        return FetchedMultiple<UserDto>(users);
    }
}

public class GetOneUserByIdHandler : GetOneQueryHandlerBase<GetOneUserByIdQuery, ApplicationUser, UserDto>
{
    public override async Task<Response<UserDto>> Handle(GetOneUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        if (user is null) return NotFound<UserDto>($"No resource found with the id = {request.Id}");
        var dto = _mapper.Map<UserDto>(user);
        return Ok200<UserDto>(dto);
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

        var roleAssignmentResult = await _userManager.AddToRoleAsync(entityToAdd, enUserRoles.Researcher.ToString());
        if (!roleAssignmentResult.Succeeded)
        {
            return BadRequest<UserDto>($"User created but failed to assign role. Errors: {roleAssignmentResult.ConvertErrorsToString()}");
        }

        var user = await _userManager.FindByEmailAsync(entityToAdd.Email!);
        var dto = _mapper.Map<UserDto>(user);
        return Created<UserDto>(dto);
    }
}

public class UpdateUserHandler : UpdateCommandHandlerBase<UpdateUserCommand, ApplicationUser, UserDto>
{

    public override async Task<Response<UserDto>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entityToUpdate = await _userManager.FindByIdAsync(request.Id);
        if (entityToUpdate is null) return NotFound<UserDto>($"No resource found with the id = {request.Id}");

        var mappedUser = _mapper.Map(request, entityToUpdate);
        var updateResult = await _userManager.UpdateAsync(mappedUser);

        if (!updateResult.Succeeded)
        {
            return BadRequest<UserDto>($"Failed to update the user. Errors: {updateResult.ConvertErrorsToString()}");
        }

        var dto = _mapper.Map<UserDto>(await _userManager.FindByEmailAsync(mappedUser.Email!));

        return Updated<UserDto>(dto);
    }
}

public class DeleteUserHandler : DeleteCommandHandlerBase<DeleteUserCommand, ApplicationUser, UserDto> 
{
    public override async Task<Response<UserDto>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entityToDelete = await _userManager.FindByIdAsync(request.Id);
        if (entityToDelete is null) return NotFound<UserDto>($"No resource found with the id = {request.Id}");

        var deleteResult = await _userManager.DeleteAsync(entityToDelete);
        if (!deleteResult.Succeeded)
        {
            return BadRequest<UserDto>($"Failed to update the user. Errors: {deleteResult.ConvertErrorsToString()}");
        }

        return Deleted<UserDto>();
    }
}