using Application.DTOs;
using System.Net.Mail;
using Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly EmailSettings _settings;

    public EmailService(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendOtpAsync(string toEmail, string maOtp)
    {
        var message = new MimeMessage();

        message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = "Mã xác thực OTP";

        message.Body = new TextPart("html")
        {
            Text = $@"
                <h3>Mã OTP của bạn</h3>
                <p>Mã OTP: <strong style='font-size:24px'>{maOtp}</strong></p>
                <p>Mã có hiệu lực trong <strong>2 phút</strong>. Không chia sẻ mã này cho ai.</p>
            "
        };

        using var client = new SmtpClient();

        await client.ConnectAsync(
            _settings.Host,
            _settings.Port,
            _settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None);

        await client.AuthenticateAsync(_settings.SenderEmail, _settings.Password);
        await client.SendAsync(message);
        await client.DisconnectAsync(true);
    }
}