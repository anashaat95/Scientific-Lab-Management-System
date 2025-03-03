namespace ScientificLabManagementApp.Application;

public class GetManyUserQuery : GetManyQueryBase<UserDto>{}
public class GetManyUserSelectOptionsQuery : GetManySelectOptionsQueryBases<SelectOptionDto> { }
public class GetManyTechnicianQuery : GetManyQueryBase<UserDto>{}
public class GetManyTechnicianSelectOptionsQuery : GetManySelectOptionsQueryBases<SelectOptionDto>{}
public class GetManyLabSupervisorQuery : GetManyQueryBase<UserDto>{}
public class GetManySupervisorSelectOptionsQuery : GetManySelectOptionsQueryBases<SelectOptionDto> { }
public class GetManyResearcherQuery : GetManyQueryBase<UserDto>{}
public class GetManyResearcherSelectOptionsQuery : GetManySelectOptionsQueryBases<SelectOptionDto> { }
public class GetOneUserByIdQuery : GetOneQueryBase<UserDto>{}

public class GetOneUserByFieldQueryBase: RoleAuthorizeRequest, IRequest<Response<UserDto>>
{

    [FromRoute]
    [Required]
    public required string Field { get; set; }

    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.AnyUserLevel;
}

public class GetOneUserByEmailQuery : GetOneQueryByEmailBase<UserDto>{}
public class ExistOneUserByEmailQuery : ExistOneQueryByEmailBase{}
