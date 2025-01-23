namespace ScientificLabManagementApp.Application;

public abstract class ValidatorBase<TRequest, TEntity, TDto> : AbstractValidator<TRequest>
    where TRequest : IRequest<Response<TDto>>
    where TEntity : class, IEntityBase
    where TDto : class, IEntityHaveId
{
    #region Fields
    protected readonly IBaseService<TEntity, TDto> _basicService;
    protected readonly UserManager<ApplicationUser> _userManager;
    protected readonly RoleManager<ApplicationRole> _roleManager;
    #endregion

    #region Constructor(s)
    public ValidatorBase()
    {
        var serviceProvider = new HttpContextAccessor().HttpContext?.RequestServices;
        _basicService = serviceProvider!.GetRequiredService<IBaseService<TEntity, TDto>>();
        _userManager = serviceProvider!.GetRequiredService<UserManager<ApplicationUser>>();
        _roleManager = serviceProvider!.GetRequiredService<RoleManager<ApplicationRole>>();

        ApplyValidationRules();
        ApplyCustomValidationRules();
    }
    #endregion

    #region Actions
    public abstract void ApplyValidationRules();
    
    public virtual void ApplyCustomValidationRules() {}
    #endregion
}
