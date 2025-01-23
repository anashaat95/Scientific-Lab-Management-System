namespace ScientificLabManagementApp.Application;
public abstract class RequestHandlerBase<TRequest, TEntity, TDto>
    : ResponseBuilder, IRequestHandler<TRequest, Response<TDto>>
    where TRequest : IRequest<Response<TDto>>
    where TEntity : class, IEntityBase
    where TDto : class, IEntityHaveId
{
    #region Field(s)
    protected readonly IBaseService<TEntity, TDto> _basicService;
    protected readonly UserManager<ApplicationUser> _userManager;
    protected readonly RoleManager<ApplicationRole> _roleManager;
    protected readonly ITokenService _tokenService;
    protected readonly IEmailService _emailService;
    protected readonly IMapper _mapper;
    protected readonly ICurrentUserService _currentUserService;
    #endregion


    #region Constructor
    protected RequestHandlerBase()
    {
        var serviceProvider = new HttpContextAccessor().HttpContext?.RequestServices;
        _basicService = serviceProvider!.GetRequiredService<IBaseService<TEntity, TDto>>();
        _userManager = serviceProvider!.GetRequiredService<UserManager<ApplicationUser>>();
        _roleManager = serviceProvider!.GetRequiredService<RoleManager<ApplicationRole>>();
        _tokenService = serviceProvider!.GetRequiredService<ITokenService>();
        _emailService = serviceProvider!.GetRequiredService<IEmailService>();
        _mapper = serviceProvider!.GetRequiredService<IMapper>();
        _currentUserService = serviceProvider!.GetRequiredService<ICurrentUserService>();
    }
    #endregion

    #region HandleRequest
    public abstract Task<Response<TDto>> Handle(TRequest request, CancellationToken cancellationToken);
    #endregion
}
