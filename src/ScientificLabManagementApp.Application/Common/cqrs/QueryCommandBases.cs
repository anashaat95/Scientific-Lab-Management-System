namespace ScientificLabManagementApp.Application;


public class RoleAuthorizeRequest : IRoleAuthorizeRequest
{
    public virtual IEnumerable<string> AllowedRoles()
    {
        return AllowedRolesFactory.AdminLevel;
    }
}

public class GetOneQueryBase<TDto> : RoleAuthorizeRequest, IRequest<Response<TDto>>, IEntityHaveId
    where TDto : class
{
    [FromRoute]
    [Required]
    public required string Id { get; set; }

    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.AnyUserLevel;
}

public class GetOneQueryByEmailBase<TDto> : RoleAuthorizeRequest, IRequest<Response<TDto>>
    where TDto : class
{
    [FromQuery]
    [Required]
    public required string Email { get; set; }
    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.AnyUserLevel;
}

public class ExistOneQueryByEmailBase : IRequest<Response<bool>>
{
    [FromQuery]
    [Required]
    public required string Email { get; set; }
}

public class GetManyQueryBases<TDto> : RoleAuthorizeRequest, IRequest<Response<IEnumerable<TDto>>>
    where TDto : class
{
    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.AnyUserLevel;
}

public class GetManySelectOptionsQueryBases<TDto> : IRequest<Response<IEnumerable<TDto>>>
    where TDto : class
{
}

public abstract class AddUpdateCommandBase<TDto, TCommandData> : RoleAuthorizeRequest, IRequest<Response<TDto>>
    where TDto : class
    where TCommandData : class
{
    [FromBody]
    public virtual TCommandData Data { get; set; }
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


