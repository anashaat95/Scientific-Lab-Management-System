namespace ScientificLabManagementApp.API;

public abstract class ApiControllerBase:ControllerBase
{
    #region Fields
    private IMediator _mediateInstance;
    protected IMediator Mediator => _mediateInstance ??= HttpContext.RequestServices.GetService<IMediator>();
    #endregion

    protected ControllerResult Result = new();

}
