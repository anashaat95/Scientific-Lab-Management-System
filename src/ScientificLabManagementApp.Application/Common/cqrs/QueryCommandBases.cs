namespace ScientificLabManagementApp.Application;

public class RoleAuthorizeRequest : IRoleAuthorizRequest
{
    public virtual IEnumerable<string> AllowedRoles => AllowedRolesFactory.AdminLevel;
}

public class GetOneQueryBase<TDto> : RoleAuthorizeRequest, IRequest<Response<TDto>>, IEntityHaveId
    where TDto : class
{
    [FromRoute]
    [Required]
    public required string Id { get; set; }


    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.AnyUserLevel;
}

public class GetManyQueryBases<TDto> : RoleAuthorizeRequest, IRequest<Response<IEnumerable<TDto>>>
    where TDto : class
{
    public override IEnumerable<string> AllowedRoles => AllowedRolesFactory.AnyUserLevel;
}

public abstract class AddUpdateCommandBase<TDto, TCommandData> : RoleAuthorizeRequest, IRequest<Response<TDto>>
    where TDto : class
    where TCommandData : class
{
    [FromBody]
    public TCommandData Data { get; set; }
}

public class AddCommandBase<TDto, TCommandData> : AddUpdateCommandBase<TDto, TCommandData>
    where TDto : class
    where TCommandData : class
{
}

public class UpdateCommandBase<TDto, TCommandData> : AddUpdateCommandBase<TDto, TCommandData>, IEntityHaveId
    where TDto : class
    where TCommandData : class
{
    [FromRoute]
    [Required]
    public required string Id { get; set; }
}

public class DeleteCommandBase<TDto> : RoleAuthorizeRequest, IRequest<Response<TDto>>, IEntityHaveId
    where TDto : class
{
    [FromRoute]
    [Required]
    public required string Id { get; set; }
}


