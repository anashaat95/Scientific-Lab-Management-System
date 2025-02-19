
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

    private string? ExtractJwtToken()
    {
        var authHeader = _httpContextAccessor?.HttpContext?.Request?.Headers["Authorization"].ToString();

        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return null;


        var tokenParts = authHeader.Split(" ");
        return tokenParts.Length > 1 ? tokenParts[1] : null;
    }

    public ClaimsPrincipal User 
    {

        get => _tokenService.GetClaimsPrincipalFromAccessToken(ExtractJwtToken());
    }

    public string UserId => User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

    public IEnumerable<string> UserRoles => User?.Claims
        .Where(c => c.Type == ClaimTypes.Role)
        .Select(c => c.Value)
        ?? Enumerable.Empty<string>();
}