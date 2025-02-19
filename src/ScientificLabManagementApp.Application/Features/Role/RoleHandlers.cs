using Azure.Core;
using MediatR;

namespace ScientificLabManagementApp.Application;
public class GetManyRoleHandler : GetManyQueryHandlerBase<GetManyRoleQuery, ApplicationRole, RoleDto>
{
    public override async Task<Response<IEnumerable<RoleDto>>> Handle(GetManyRoleQuery request, CancellationToken cancellationToken)
    {
        var users = await _roleManager.Roles
                                .ProjectTo<RoleDto>(_mapper.ConfigurationProvider)
                                .ToListAsync();

        return FetchedMultiple<RoleDto>(users);
    }
}

public class GetOneRoleByIdHandler : GetOneQueryHandlerBase<GetOneRoleByIdQuery, ApplicationRole, RoleDto>
{
    public override async Task<Response<RoleDto>> Handle(GetOneRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _roleManager.FindByIdAsync(request.Id);
        if (entity is null) return NotFound<RoleDto>($"No resource found with the id = {request.Id}");
        var dto = _mapper.Map<RoleDto>(entity);
        return Ok200<RoleDto>(dto);
    }
}

public class AddRoleHandler : AddCommandHandlerBase<AddRoleCommand, ApplicationRole, RoleDto>
{
    public override async Task<Response<RoleDto>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var entityToAdd = _mapper.Map<ApplicationRole>(request);

        var creationResult = await _roleManager.CreateAsync(entityToAdd);
        if (!creationResult.Succeeded)
        {
            return BadRequest<RoleDto>($"Failed to add the user. Errors: {creationResult.ConvertErrorsToString()}");
        }

        var dto = _mapper.Map<RoleDto>(await _roleManager.FindByNameAsync(entityToAdd.Name!));
        return Created<RoleDto>(dto);
    }
}

public class UpdateRoleHandler : UpdateCommandHandlerBase<UpdateRoleCommand, ApplicationRole, RoleDto>
{

    protected override async Task<Response<RoleDto>> DoUpdate(UpdateRoleCommand updateRequest, ApplicationRole entityToUpdate)
    {
        var mappedRole = _mapper.Map(updateRequest, entityToUpdate);
        var updateResult = await _roleManager.UpdateAsync(mappedRole);

        if (!updateResult.Succeeded)
        {
            return BadRequest<RoleDto>($"Failed to update the user. Errors: {updateResult.ConvertErrorsToString()}");
        }

        var dto = _mapper.Map<RoleDto>(await _roleManager.FindByNameAsync(mappedRole.Name!));

        return Updated<RoleDto>(dto);
    }
}

public class DeleteRoleHandler : DeleteCommandHandlerBase<DeleteRoleCommand, ApplicationRole, RoleDto>
{
    protected override async Task<Response<RoleDto>> DoDelete(ApplicationRole entityToDelete)
    {
        var deleteResult = await _roleManager.DeleteAsync(entityToDelete);
        if (!deleteResult.Succeeded)
        {
            return BadRequest<RoleDto>($"Failed to update the user. Errors: {deleteResult.ConvertErrorsToString()}");
        }

        return Deleted<RoleDto>();
    }
}