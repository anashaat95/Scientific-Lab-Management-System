namespace ScientificLabManagementApp.Application;

public class GetManyLabQuery : GetManyQueryBase<LabDto>{}
public class GetManyLabSelectOptionsQuery : GetManySelectOptionsQueryBases<SelectOptionDto> { }
public class GetOneLabByIdQuery : GetOneQueryBase<LabDto>{}

public class GetOneLabByNameQuery : RoleAuthorizeRequest, IRequest<Response<LabDto>>
{
    [FromRoute]
    [Required]
    public required string Name { get; set; }
    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.AnyUserLevel;
}