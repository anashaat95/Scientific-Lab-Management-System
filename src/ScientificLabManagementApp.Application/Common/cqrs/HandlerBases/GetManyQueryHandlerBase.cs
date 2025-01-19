namespace ScientificLabManagementApp.Application;

public class GetManyQueryHandlerBase<TRequest, TEntity, TDto> : ResponseBuilder, IRequestHandler<TRequest, Response<IEnumerable<TDto>>>
    where TRequest : IRequest<Response<IEnumerable<TDto>>>
    where TEntity : class
    where TDto : class
{
    protected readonly IBaseService<TEntity, TDto> _basicService;
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
    }

    public virtual async Task<Response<IEnumerable<TDto>>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var entities = await _basicService.GetAllAsync();
        return FetchedMultiple(entities);
    }
}