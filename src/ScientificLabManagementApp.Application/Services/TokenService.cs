using System.Security.Cryptography;

namespace ScientificLabManagementApp.Application;
public class TokenService : ITokenService
{
    private readonly IConfiguration _configManager;
    private readonly JwtSettings _jwtSettings;
    public int AccessTokenExpirationInMinutes { get; }
    public int AccessTokenExpirationInDaysIfRememberMe { get; }
    public int RefreshTokenExpirationInDays { get; }

    public TokenService(IConfiguration configManager, IOptions<JwtSettings> jwtSettings)
    {
        _configManager = configManager;
        _jwtSettings = jwtSettings.Value;
        AccessTokenExpirationInMinutes = _jwtSettings.AccessTokenExpiresInMinutes;
        AccessTokenExpirationInDaysIfRememberMe = _jwtSettings.AccessTokenExpirationInDaysIfRememberMe;
        RefreshTokenExpirationInDays = _jwtSettings.RefreshTokenExpiresInDays;
    }

    private SymmetricSecurityKey GetKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configManager["SECURITY_KEY"]));
    }

    public ClaimsPrincipal GetClaimsPrincipalFromAccessToken(string? token)
    {
        if (string.IsNullOrEmpty(token)) return new ClaimsPrincipal();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = _jwtSettings.ValidateAudience,
            ValidateIssuer = _jwtSettings.ValidateIssuer,
            ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
            IssuerSigningKey = GetKey(),
            ValidateLifetime = _jwtSettings.ValidateLifeTime,
            ValidIssuers = new[] { _jwtSettings.Issuer },
            ValidAudience = _jwtSettings.Audience,
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        SecurityToken securityToken;
        ClaimsPrincipal claimsPrincipal;
        try
        {
            claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
        }
        catch (SecurityTokenExpiredException ex)
        {
            throw new SecurityTokenExpiredException($"Token has expired. Expiry time (UTC): {ex.Expires}");
        }
        catch (SecurityTokenValidationException ex)
        {
            throw new SecurityTokenValidationException($"Token validation failed: {ex.Message}");
            // Handle general validation errors
        }

        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            throw new SecurityTokenException("Invalid token");

        return claimsPrincipal;
    }

    public string GenerateAccessToken(ApplicationUser user, IEnumerable<string> userRoles, bool rememberMe = false)
    {
        var creds = new SigningCredentials(GetKey(), SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.NameId, user.Id),
        };

        foreach (var role in userRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var expires = rememberMe ? 
            DateTime.UtcNow.AddDays(AccessTokenExpirationInDaysIfRememberMe) : 
            DateTime.UtcNow.AddMinutes(AccessTokenExpirationInMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims, expires,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var rndNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(rndNumber);
        }

        return Convert.ToBase64String(rndNumber);
    }
}
