namespace Application.Interfaces;

public interface IEmailService
{
    Task SendOtpAsync(string toEmail, string maOtp);
}

public interface ISmsService
{
    Task SendOtpAsync(string phoneNumber, string maOtp);
}