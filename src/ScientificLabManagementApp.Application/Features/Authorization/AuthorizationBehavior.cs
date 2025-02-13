namespace ScientificLabManagementApp.Application;
public class AuthorizationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ICurrentUserService _currentUserService;

    public AuthorizationBehaviour(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (typeof(TRequest).Name == nameof(GetManyCompanyQuery) ||
            typeof(TRequest).Name == nameof(GetManyDepartmentQuery) ||
            typeof(TRequest).Name == nameof(GetManyLabQuery))
        {
            return await next();
        }

        //if (request is IRoleAuthorizRequest authorizeRequest)
        //{
        //    var user = _currentUserService.User;
        //    var userRoles = _currentUserService.UserRoles;

        //    if (!authorizeRequest.AllowedRoles.Any(role => userRoles.Contains(role)))
        //    {
        //        throw new UnauthorizedAccessException($"Access denied. Allowed roles: {string.Join(", ", authorizeRequest.AllowedRoles)}");
        //    }
        //}

        return await next();
    }
}
