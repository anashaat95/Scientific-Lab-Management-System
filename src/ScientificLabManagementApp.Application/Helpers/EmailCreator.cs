namespace ScientificLabManagementApp.Application;

public class EmailCreator
{
    public string To { get; private set; }
    public string Title { get; private set; }
    public string Body { get; private set; }

    protected readonly IUrlService _urlService;

    public EmailCreator(IUrlService urlService)
    {
        _urlService = urlService;
    }

    private string GetUrl(ApplicationUser user, string token)
    {
        return $"{_urlService.GetBaseUrl()}/verify-email?user_id={user.Id}&token={token}"; 
    }

    public void CreateEmailVerificationConfirmationEmail(ApplicationUser user, string token)
    {
        this.To = user.Email;
        this.Title = "Verify Your Email";
        this.Body = $"<p>Dear {user.FirstName},</p>" +
                    $"<p>Please verify your email by clicking the link below:</p>" +
                    $"<a href='{GetUrl(user, token)}'>Verify Email</a>";
    }

    public void CreateUpdateEmailVerificationConfirmationEmail(ApplicationUser user, string token)
    {
        this.To = user.Email;
        this.Title = "Verify Your Updated Email";
        this.Body = $"<p>Dear {user.FirstName},</p>" +
                    $"<p>Please verify your update email by clicking the link below:</p>" +
                    $"<a href='{_urlService.GetBaseUrl()}/verify-update-email?user_id={user.Id}&token={token}&new_email={user.Email}'>Verify Email</a>";
    }
    

    public void CreatePasswordResetEmail(ApplicationUser user, string token)
    {
        this.To = user.Email;
        this.Title = "Password Reset Request";
        this.Body = $"Please reset your password by clicking <a href='{GetUrl(user, token)}'>here</a>.";
    }
}
