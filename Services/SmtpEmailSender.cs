using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace Huynh_Quang_Tuan_Nhom_Dasani_Sang_Thu_5.Services;

public sealed class EmailSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 587;
    public bool EnableSsl { get; set; } = true;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FromEmail { get; set; } = string.Empty;
    public string FromName { get; set; } = "Attendance App";
}

public interface IEmailSender
{
    Task SendOtpAsync(string recipient, string code, CancellationToken cancellationToken);
}

public sealed class EmailNotConfiguredException(string message) : Exception(message);

public sealed class SmtpEmailSender(IOptions<EmailSettings> options) : IEmailSender
{
    private readonly EmailSettings _settings = options.Value;

    public async Task SendOtpAsync(string recipient, string code, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_settings.Host) ||
            string.IsNullOrWhiteSpace(_settings.FromEmail) ||
            string.IsNullOrWhiteSpace(_settings.UserName) ||
            string.IsNullOrWhiteSpace(_settings.Password))
        {
            throw new EmailNotConfiguredException(
                "SMTP is not configured. Set Email__Host, Email__Port, Email__EnableSsl, Email__UserName, Email__Password, and Email__FromEmail.");
        }

        using var message = new MailMessage
        {
            From = new MailAddress(_settings.FromEmail, _settings.FromName),
            Subject = "Your attendance app verification code",
            Body = $"Your one-time login code is {code}. It expires in 5 minutes.",
            IsBodyHtml = false
        };
        message.To.Add(recipient);

        using var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            EnableSsl = _settings.EnableSsl,
            Credentials = new NetworkCredential(_settings.UserName, _settings.Password)
        };
        await client.SendMailAsync(message, cancellationToken);
    }
}
