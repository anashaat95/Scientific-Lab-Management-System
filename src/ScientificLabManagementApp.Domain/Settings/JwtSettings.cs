namespace ScientificLabManagementApp.Domain;

public class JwtSettings
{
    public string Issuer { get; set; } = String.Empty;
    public string Audience { get; set; } = String.Empty;
    public bool ValidateAudience { get; set; }
    public bool ValidateIssuer { get; set; }
    public bool ValidateLifeTime { get; set; }
    public bool ValidateIssuerSigningKey { get; set; }
    public int AccessTokenExpiresInMinutes { get; set; }
    public int AccessTokenExpirationInDaysIfRememberMe { get; set; }
    public int RefreshTokenExpiresInDays { get; set; }
}