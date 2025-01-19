namespace ScientificLabManagementApp.Application;
public class TokenDto
{
    public string Token { get; set; }
    public DateTime ExpiresIn { get; set; }
}

public class LoginDto
{
    public TokenDto AccessToken { get; set; }
    public TokenDto RefreshToken { get; set; }
}


public class SignupDto
{
    public string UserId { get; set; }
}


