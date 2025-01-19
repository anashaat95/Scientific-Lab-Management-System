namespace ScientificLabManagementApp.Application;

public interface ITokenService
{
    string GenerateAccessToken(ApplicationUser User, IEnumerable<string> userRoles);
    string GenerateRefreshToken();
    ClaimsPrincipal GetClaimsPrincipalFromAccessToken(string token);
    int AccessTokenExpirationInMinutes { get; }
    int RefreshTokenExpirationInDays { get; }
}
