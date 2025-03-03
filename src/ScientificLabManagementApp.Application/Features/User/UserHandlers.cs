
using Azure.Core;

namespace ScientificLabManagementApp.Application;
public class GetManyUserHandler : GetManyQueryHandlerBase<GetManyUserQuery, ApplicationUser, UserDto>
{
    public override async Task<Response<IEnumerable<UserDto>>> Handle(GetManyUserQuery request, CancellationToken cancellationToken)
    {
        var parameters = _mapper.Map<AllResourceParameters>(request);
        var result = await _applicationUserService.GetAllUsersByRoleAsync(null, parameters);
        return FetchedMultiple(result.Items, result.Meta);
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
    public override async Task<Response<IEnumerable<UserDto>>> Handle(GetManyTechnicianQuery request, CancellationToken cancellationToken)
    {
        var parameters = _mapper.Map<AllResourceParameters>(request);
        var result = await _applicationUserService.GetAllUsersByRoleAsync(enUserRoles.Technician.ToString(), parameters);
        return FetchedMultiple(result.Items, result.Meta);
    }
}

public class GetManyTechnicianSelectOptionsHandler : GetManySelectOptionsQueryHandler<GetManyTechnicianSelectOptionsQuery, MappingApplicationUser>
{
    protected override Task<IEnumerable<SelectOptionDto>> GetEntityDtos()
    {
        return _applicationUserService.GetAllUsersSelectOptionsByRoleAsync(enUserRoles.Technician.ToString());
    }
}

public class GetManyLabSupervisorHandler : GetManyQueryHandlerBase<GetManyLabSupervisorQuery, ApplicationUser, UserDto>
{
    public override async Task<Response<IEnumerable<UserDto>>> Handle(GetManyLabSupervisorQuery request, CancellationToken cancellationToken)
    {
        var parameters = _mapper.Map<AllResourceParameters>(request);
        var result = await _applicationUserService.GetAllUsersByRoleAsync(enUserRoles.LabSupervisor.ToString(), parameters);
        return FetchedMultiple(result.Items, result.Meta);
    }
}

public class GetManyLabSupervisorSelectOptionsHandler : GetManySelectOptionsQueryHandler<GetManySupervisorSelectOptionsQuery, MappingApplicationUser>
{
    protected override Task<IEnumerable<SelectOptionDto>> GetEntityDtos()
    {
        return _applicationUserService.GetAllUsersSelectOptionsByRoleAsync(enUserRoles.LabSupervisor.ToString());
    }
}

public class GetManyResearcherHandler : GetManyQueryHandlerBase<GetManyResearcherQuery, ApplicationUser, UserDto>
{
    public override async Task<Response<IEnumerable<UserDto>>> Handle(GetManyResearcherQuery request, CancellationToken cancellationToken)
    {
        var parameters = _mapper.Map<AllResourceParameters>(request);
        var result = await _applicationUserService.GetAllUsersByRoleAsync(enUserRoles.Researcher.ToString(), parameters);
        return FetchedMultiple(result.Items, result.Meta);
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

public class GetOneUserByUsernameHandler : RequestHandlerBase<GetOneUserByFieldQueryBase, MappingApplicationUser, UserDto>
{
    public override async Task<Response<UserDto>> Handle(GetOneUserByFieldQueryBase request, CancellationToken cancellationToken)
    {
        var entityDto = await GetEntityDto(request);
        return entityDto is not null ? Ok200(entityDto) : NotFound<UserDto>($"No resource found with this field = {request.Field}");
    }

    protected virtual Task<UserDto?> GetEntityDto(GetOneUserByFieldQueryBase request)
    {
        return _applicationUserService.FindOneAsync(u => u.UserName == request.Field);
    }
}


public class GetOneUserByEmailHandler : ResponseBuilder, IRequestHandler<GetOneUserByEmailQuery, Response<UserDto>>
{
    protected readonly IApplicationUserService _applicationUserService;

    #region Constructor
    public GetOneUserByEmailHandler(IApplicationUserService applicationUserService)
    {
        _applicationUserService = applicationUserService;
    }
    #endregion
    public async Task<Response<UserDto>> Handle(GetOneUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var entityDto = await _applicationUserService.GetOneByEmailAsync(request.Email);
        return entityDto is not null ? Ok200(entityDto) : NotFound<UserDto>($"No resource found with Id = {request.Email}");
    }
}

public class ExistOneUserByEmailHandler : ResponseBuilder, IRequestHandler<ExistOneUserByEmailQuery, Response<bool>>
{
    protected readonly IBaseService<ApplicationUser, UserDto> _baseService;

    #region Constructor
    public ExistOneUserByEmailHandler(IBaseService<ApplicationUser, UserDto> baseService)
    {
        _baseService = baseService;
    }
    #endregion

    public async Task<Response<bool>> Handle(ExistOneUserByEmailQuery request, CancellationToken cancellationToken)
    {
        var found = await _baseService.ExistsAsync(e => e.NormalizedEmail == request.Email);
        return found ? Ok200<bool>(found) : NotFound<bool>();
    }
}


public class AddUserHandler : AddCommandHandlerBase<AddUserCommand, ApplicationUser, UserDto>
{

    public override async Task<Response<UserDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var entityToAdd = _mapper.Map<ApplicationUser>(request);

        if (request.Data.image != null) entityToAdd.ImageUrl = await _cloudinaryService.GetUrlOfUploadedImage(request.Data.image);

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

        if (request.Data.image != null)  mappedUserEntity.ImageUrl = await _cloudinaryService.GetUrlOfUploadedImage(request.Data.image);

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