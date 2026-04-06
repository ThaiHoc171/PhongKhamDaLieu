using Domain.Entities;

namespace Application.Interfaces;

public interface IOtpRepository
{
    Task<int> AddAsync(Otp otp);
    Task<Otp?> GetValidOtpByTaiKhoanAsync(int taiKhoanID, string maOTP);
    Task<Otp?> GetValidOtpByEmailAsync(string email, string maOTP);  
    Task InvalidateAllByTaiKhoanAsync(int taiKhoanID);
    Task InvalidateAllByEmailAsync(string email);                    
    Task InvalidateAsync(int otpID);
}