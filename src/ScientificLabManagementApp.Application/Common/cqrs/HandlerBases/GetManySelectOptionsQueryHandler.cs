namespace ScientificLabManagementApp.Application;

public class GetManySelectOptionsQueryHandler<TRequest, TEntity> : ResponseBuilder, IRequestHandler<TRequest, Response<IEnumerable<SelectOptionDto>>>
    where TRequest : IRequest<Response<IEnumerable<SelectOptionDto>>>
    where TEntity : class, IEntityBase
{
    protected readonly IBaseService<TEntity, SelectOptionDto> _basicService;
    protected readonly IApplicationUserService _applicationUserService;
    protected readonly UserManager<ApplicationUser> _userManager;
    protected readonly RoleManager<ApplicationRole> _roleManager;
    protected readonly IMapper _mapper;

    public GetManySelectOptionsQueryHandler()
    {
        var serviceProvider = new HttpContextAccessor().HttpContext?.RequestServices;
        _basicService = serviceProvider!.GetRequiredService<IBaseService<TEntity, SelectOptionDto>>();
        _userManager = serviceProvider!.GetRequiredService<UserManager<ApplicationUser>>();
        _roleManager = serviceProvider!.GetRequiredService<RoleManager<ApplicationRole>>();
        _mapper = serviceProvider!.GetRequiredService<IMapper>();
        _applicationUserService = serviceProvider!.GetRequiredService<IApplicationUserService>();
    }

    public virtual async Task<Response<IEnumerable<SelectOptionDto>>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var entities = await GetEntityDtos();
        return FetchedMultiple(entities);
    }

    protected virtual Task<IEnumerable<SelectOptionDto>> GetEntityDtos()
    {
        return _basicService.GetSelectOptionsAsync(e => true);
    }
}
