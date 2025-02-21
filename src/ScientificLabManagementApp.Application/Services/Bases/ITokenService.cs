namespace ScientificLabManagementApp.Application;

public interface ITokenService
{
    string GenerateAccessToken(ApplicationUser User, IEnumerable<string> userRoles, bool rememberMe = false);
    string GenerateRefreshToken();
    ClaimsPrincipal GetClaimsPrincipalFromAccessToken(string token);
    int AccessTokenExpirationInMinutes { get; }
    int AccessTokenExpirationInDaysIfRememberMe { get; }
    int RefreshTokenExpirationInDays { get; }
}
