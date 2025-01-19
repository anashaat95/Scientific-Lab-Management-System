
namespace ScientificLabManagementApp.Application;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ITokenService _tokenService;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor, ITokenService serviceToken)
    {
        _httpContextAccessor = httpContextAccessor;
        _tokenService = serviceToken;
    }

    public ClaimsPrincipal User 
    {
        get {
            var authHeader = _httpContextAccessor?.HttpContext?.Request?.Headers["Authorization"].ToString();

            var token = String.Empty;
            if (authHeader.Split(" ").Length > 1)
                token = authHeader.Split(" ")[1];
            else
                throw new UnauthorizedAccessException("Invalid token");

            return _tokenService.GetClaimsPrincipalFromAccessToken(token);
        }
    }

    public string UserId => User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

    public IEnumerable<string> UserRoles => User?.Claims
        .Where(c => c.Type == ClaimTypes.Role)
        .Select(c => c.Value)
        ?? Enumerable.Empty<string>();
}