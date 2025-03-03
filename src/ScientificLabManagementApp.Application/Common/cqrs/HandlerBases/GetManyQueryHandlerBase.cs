namespace ScientificLabManagementApp.Application;

public class GetManyQueryHandlerBase<TRequest, TEntity, TDto> : ResponseBuilder, IRequestHandler<TRequest, Response<IEnumerable<TDto>>>
    where TRequest : GetManyQueryBase<TDto>
    where TEntity : class, IEntityBase
    where TDto : class, IEntityHaveId
{
    protected readonly IBaseService<TEntity, TDto> _basicService;
    protected readonly IApplicationUserService _applicationUserService;
    protected readonly UserManager<ApplicationUser> _userManager;
    protected readonly RoleManager<ApplicationRole> _roleManager;
    protected readonly IMapper _mapper;

    public GetManyQueryHandlerBase()
    {
        var serviceProvider = new HttpContextAccessor().HttpContext?.RequestServices;
        _basicService = serviceProvider!.GetRequiredService<IBaseService<TEntity, TDto>>();
        _userManager = serviceProvider!.GetRequiredService<UserManager<ApplicationUser>>();
        _roleManager = serviceProvider!.GetRequiredService<RoleManager<ApplicationRole>>();
        _mapper = serviceProvider!.GetRequiredService<IMapper>();
        _applicationUserService = serviceProvider!.GetRequiredService<IApplicationUserService>();
    }

    public virtual async Task<Response<IEnumerable<TDto>>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var result = await GetEntityDtos(request);
        return FetchedMultiple(result.Items, result.Meta);
    }

    protected virtual Task<PaginationResult<TEntity, TDto>> GetEntityDtos(TRequest request)
    {
        var parameters = _mapper.Map<AllResourceParameters>(request);

        return _basicService.GetAllAsync(parameters);
    }
}
