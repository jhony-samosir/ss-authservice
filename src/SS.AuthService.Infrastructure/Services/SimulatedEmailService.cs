using Microsoft.Extensions.Logging;
using SS.AuthService.Application.Interfaces;

namespace SS.AuthService.Infrastructure.Services;

/// <summary>
/// Simulasi pengiriman email untuk development. Ganti dengan implementasi nyata (SMTP/SendGrid)
/// di production dengan cara mendaftarkan implementasi lain di DependencyInjection.cs.
/// </summary>
public class SimulatedEmailService : IEmailService
{
    private readonly ILogger<SimulatedEmailService> _logger;

    public SimulatedEmailService(ILogger<SimulatedEmailService> logger)
    {
        _logger = logger;
    }

    public Task SendVerificationEmailAsync(string email, string token)
    {
        // Gunakan ILogger, bukan Console.WriteLine, agar terintegrasi dengan sistem logging terpusat
        _logger.LogInformation(
            "[EMAIL SIMULATION] Verification email sent to {Email}. " +
            "Link: https://samstore.com/verify-email?token={Token}",
            email, token);

        return Task.CompletedTask;
    }
}
