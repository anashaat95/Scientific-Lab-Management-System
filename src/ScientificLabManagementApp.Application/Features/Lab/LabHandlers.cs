
namespace ScientificLabManagementApp.Application;
public class GetManyLabHandler : GetManyQueryHandlerBase<GetManyLabQuery, Lab, LabDto>
{
    protected override Task<IEnumerable<LabDto>> GetEntityDtos()
    {
        return _basicService.GetAllAsync(e => e.Supervisior, e => e.Department);
    }

}

public class GetManyLabSelectOptionsHandler : GetManySelectOptionsQueryHandler<GetManyLabSelectOptionsQuery, Lab> { }

public class GetOneLabByIdHandler : GetOneQueryHandlerBase<GetOneLabByIdQuery, Lab, LabDto> {
    protected override Task<LabDto?> GetEntityDto(GetOneLabByIdQuery request)
    {
        return _basicService.GetDtoByIdAsync(request.Id, e => e.Supervisior, e => e.Department);
    }
}

public class AddLabHandler : AddCommandHandlerBase<AddLabCommand, Lab, LabDto>
{
    public override async Task<Response<LabDto>> Handle(AddLabCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Data.supervisor_id);

        if (user == null)
            return BadRequest<LabDto>($"Failed to add the lab because there is no user with the provided supervisor_id");

        var userRoles = await _userManager.GetRolesAsync(user);

        if (!userRoles.Contains(enUserRoles.LabSupervisor.ToString()))
        {
            var addRoleResult = await _userManager.AddToRoleAsync(user, enUserRoles.LabSupervisor.ToString());

            if (!addRoleResult.Succeeded)
                return BadRequest<LabDto>($"Failed to add the lab because {addRoleResult.ConvertErrorsToString()} {enUserRoles.LabSupervisor.ToString()} role to the provided user with supervisor_id");
        }

        var entityToAdd = _mapper.Map<Lab>(request);
        var resultDto = await _basicService.AddAsync(entityToAdd);

        return Created(resultDto);
    }
}

public class DeleteLabHandler : DeleteCommandHandlerBase<DeleteLabCommand, Lab, LabDto> { }

public class UpdateLabHandler : UpdateCommandHandlerBase<UpdateLabCommand, Lab, LabDto> { }
