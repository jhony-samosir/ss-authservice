namespace SS.AuthService.Infrastructure.Services;

public class EmailOptions
{
    public const string SectionName = "Email";
    public string FromEmail { get; set; } = string.Empty;
    public string SmtpServer { get; set; } = "smtp.gmail.com";
    public int Port { get; set; } = 587;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string BaseUrl { get; set; } = "https://localhost:7000";
}
