using Microsoft.AspNetCore.WebUtilities;
using System.Reflection.Metadata;

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
        this.To = user.Email!;
        this.Title = "Confirm Your Email";
        string Message = $@"Welcome to <strong style = ""box-sizing: border-box; margin: 0; padding: 0""> Pharmaceutics Scientific Lab</strong>.
                            Please click below to activate your account and complete your sign-up process: ";
        this.Body = GenerateEmailBody(
             user.FirstName + " " + user.LastName,
           "Confirm Email",
            Message,
            "Confirm Email",
            GetConfirmationUrl(user, token)
            );
    }

    public void CreateUpdateEmailVerificationConfirmationEmail(ApplicationUser user, string token)
    {
        this.To = user.Email!;
        this.Title = "Confirm Email Update";
        string Message = $@"We received a request to update your email address for your account on
                  <strong style = ""box-sizing: border-box; margin: 0; padding: 0""> Pharmaceutics Scientific Lab</strong>.Click the button below to update your the email: ";
        this.Body = GenerateEmailBody(
             user.FirstName + " " + user.LastName,
           "Confirm Email Update",
            Message,
            "Confirm Email Update",
            GetConfirmationUrl(user, token, eUrlType.UpdateEmailConfirmation)
            );
    }


    public void CreatePasswordResetEmail(ApplicationUser user, string token)
    {
        this.To = user.Email!;
        this.Title = "Password Reset Request";
        string Message = $@"We received a request to reset your password for your account on
                  <strong style = ""box-sizing: border-box; margin: 0; padding: 0""> Pharmaceutics Scientific Lab</strong>.Click the button below to reset your password: ";
        this.Body = GenerateEmailBody(
            user.FirstName + " " + user.LastName,
            "Password Reset Request",
            Message,
            "Reset",
            GetConfirmationUrl(user, token, eUrlType.PasswordReset)
            );
    }

    private static string GenerateEmailBody(string UserName, string MessageTitle, string Message, string actionText, string actionLink)
    {
        return $@"<!DOCTYPE html>
            <html>
              <head>
                <title>{MessageTitle}</title>
              </head>
              <body style=""margin: 0; padding: 0; background-color: #ffffff; font-family: 'Open Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif"">
                <table role=""presentation"" width=""100%"" cellspacing=""0"" cellpadding=""0"" border=""0"" style=""background-color: #f5f5f5; padding: 20px"">
                  <tr>
                    <td align=""center"">
                      <table
                        role=""presentation""
                        width=""600""
                        cellspacing=""0""
                        cellpadding=""0""
                        border=""0""
                        style=""background: #ffffff; border-radius: 10px; padding: 20px""
                      >
                        <tr>
                          <td align=""center"">
                            <img src=""https://i.imgur.com/YKf6w5k.png"" alt=""Pharmaceutics Scientific Lab"" width=""250"" />
                          </td>
                        </tr>

                        <tr>
                          <td align=""left"" style=""padding: 10px 0"">
                            <h2 style=""margin: 0; font-size: 32px; color: #333"">{MessageTitle}</h2>
                          </td>
                        </tr>

                        <!-- Greeting -->
                        <tr>
                          <td style=""padding: 10px; font-size: 16px; color: #333"">Dear <strong>{UserName}</strong>,</td>
                        </tr>

                        <tr>
                          <td style=""padding: 10px; font-size: 16px; color: #333"">{Message}</td>
                        </tr>

                        {GenerateActionButton(actionText, actionLink)}

                        <tr>
                          <td align=""center"" style=""padding: 10px; font-size: 14px; color: #666"">
                            <em>If you did not request this, please ignore this email.</em>
                          </td>
                        </tr>

                        <tr>
                          <td align=""center"" style=""padding: 20px; font-size: 14px; color: #333"">
                            Best regards, <br />
                            <strong>Pharmaceutics Scientific Lab</strong>
                          </td>
                        </tr>
                      </table>
                    </td>
                  </tr>
                </table>
              </body>
            </html>
";
    }
   

    private static string GenerateActionButton(string actionText, string actionLink)
    {
        if (String.IsNullOrWhiteSpace(actionLink) || String.IsNullOrEmpty(actionLink) &&
            String.IsNullOrWhiteSpace(actionText) || String.IsNullOrEmpty(actionText))
            return "";


        return $@"<tr>
              <td align=""center"" style=""padding: 20px"">
                <a
                  href=""{actionLink}""
                  style=""
                    background-color: #007bff;
                    color: white;
                    text-decoration: none;
                    padding: 12px 24px;
                    border-radius: 5px;
                    font-size: 16px;
                    display: inline-block;
                  ""
                >
                  {actionText}
                </a>
              </td>
            </tr>";
    }
}
