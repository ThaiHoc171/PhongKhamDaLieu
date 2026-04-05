using Domain.Entities;

namespace Application.Interfaces;

public interface IOtpRepository
{
    Task<int> AddAsync(Otp otp);
    Task<Otp?> GetValidOtpAsync(int taiKhoanID, string maOTP);
    Task InvalidateAllAsync(int taiKhoanID);
    Task InvalidateAsync(int otpID);
}