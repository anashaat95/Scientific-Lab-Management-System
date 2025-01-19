namespace ScientificLabManagementApp.Domain;

public class RefreshToken : EntityBase
{
    public string Token { get; set; }
    public DateTime ExpiresIn { get; set; }
    public string UserId { get; set; }
    public DateTime? RevokedAt { get; set; }
    public bool IsRevoked { get; set; } 

    public bool IsExpired => DateTime.UtcNow >= ExpiresIn;
}
