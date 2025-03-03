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

public class GetManyQueryBase<TDto> : RoleAuthorizeRequest, IRequest<Response<IEnumerable<TDto>>>
    where TDto : class
{
    public override IEnumerable<string> AllowedRoles() => AllowedRolesFactory.AnyUserLevel;

    public string? Filter { get; set; } = null;
    public string? SortBy { get; set; } = null;
    public string? SearchQuery { get; set; } = null;
    public bool Descending { get; set; } = false;
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
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


