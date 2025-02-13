using Microsoft.AspNetCore.WebUtilities;

namespace ScientificLabManagementApp.Application;

enum eUrlType
{
    EmailConfirmation,
    UpdateEmailConfirmation,
    PasswordReset
}
public class EmailCreator
{
    public string To { get; private set; }
    public string Title { get; private set; }
    public string Body { get; private set; }

    protected readonly IUrlService _urlService;
    protected readonly IConfiguration _configManager;

    public EmailCreator(IUrlService urlService, IConfiguration configManager)
    {
        _urlService = urlService;
        _configManager = configManager;
    }


    private string GetConfirmationUrl(ApplicationUser user, string token, eUrlType urlType = eUrlType.EmailConfirmation)
    {
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        switch (urlType)
        {
            case eUrlType.EmailConfirmation:
                return $"{_configManager["FrontendUrl"]}/confirm-email?user_id={user.Id}&token={encodedToken}";
            case eUrlType.UpdateEmailConfirmation:
                return $"{_configManager["FrontendUrl"]}/confirm-update-email?user_id={user.Id}&token={encodedToken}&new_email={user.Email}";
            case eUrlType.PasswordReset:
                return $"{_configManager["FrontendUrl"]}/reset-password?user_id={user.Id}&token={encodedToken}";
            default:
                return $"{_configManager["FrontendUrl"]}/confirm-email?user_id={user.Id}&token={encodedToken}";
        }
    }

    public void CreateEmailVerificationConfirmationEmail(ApplicationUser user, string token)
    {
        this.To = user.Email;
        this.Title = "Confirm Your Email";
        this.Body = $"<p>Dear {user.FirstName},</p>" +
                    $"<p>Please cerify your email by clicking the link below:</p>" +
                    $"<a href='{GetConfirmationUrl(user, token)}'>Confirm Email</a>";
    }

    public void CreateUpdateEmailVerificationConfirmationEmail(ApplicationUser user, string token)
    {
        this.To = user.Email;
        this.Title = "confirm Your Updated Email";
        this.Body = $"<p>Dear {user.FirstName},</p>" +
                    $"<p>Please confirm your update email by clicking the link below:</p>" +
                    $"<a href='{GetConfirmationUrl(user, token, eUrlType.UpdateEmailConfirmation)}";
    }


    public void CreatePasswordResetEmail(ApplicationUser user, string token)
    {
        this.To = user.Email;
        this.Title = "Password Reset Request";
        this.Body = $"Please reset your password by clicking <a href='{GetConfirmationUrl(user, token, eUrlType.PasswordReset)}'>here</a>.";
    }
}
