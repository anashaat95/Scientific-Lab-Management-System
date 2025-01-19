namespace ScientificLabManagementApp.Application;

public class EmailService : IEmailService
{
    private readonly SmtpSettings _stmpSettings;
    private readonly IConfiguration _configManager;

    public EmailService(IOptions<SmtpSettings> gmailSettings, IConfiguration configManager)
    {
        _stmpSettings = gmailSettings.Value;
        _configManager = configManager;
    }
    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        if (string.IsNullOrWhiteSpace(toEmail))
        {
            throw new ArgumentException("Recipient email address is required.", nameof(toEmail));
        }

        var apiKey = _configManager["SendGrid_API_KEY"];



        var client = new SendGridClient(apiKey);
        var from = new EmailAddress(_stmpSettings.FromAddress, _stmpSettings.FromName);
        var to = new EmailAddress(toEmail);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, body, body);
        var response = await client.SendEmailAsync(msg);

        if (!response.IsSuccessStatusCode)
        {
            var errorBody = await response.Body.ReadAsStringAsync();
            throw new Exception($"Email sending failed: {response.StatusCode}, {errorBody}");
        }
    }
}
