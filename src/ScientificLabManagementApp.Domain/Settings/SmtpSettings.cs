namespace ScientificLabManagementApp.API;

public class SmtpSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public bool EnableSsl { get; set; }
    public string FromAddress { get; set; }
    public string FromName { get; set; }
}
