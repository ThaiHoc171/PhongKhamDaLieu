using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class OtpService
{
    private readonly IOtpRepository _otpRepo;
    private readonly ITaiKhoanRepository _taiKhoanRepo;
    private readonly IEmailService _emailService;

    public OtpService(
        IOtpRepository otpRepo,
        ITaiKhoanRepository taiKhoanRepo,
        IEmailService emailService)
    {
        _otpRepo = otpRepo;
        _taiKhoanRepo = taiKhoanRepo;
        _emailService = emailService;
    }

    public async Task<ApiResponse<bool>> TaoOtpAsync(TaoOtpRequestDTO dto)
    {
        try
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email))
                return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

            var email = dto.Email.Trim().ToLower();
            var maOtp = TaoMaOtp();
            var taiKhoan = await _taiKhoanRepo.GetByEmailAsync(email);

            if (taiKhoan != null)
            {
                // Tài khoản đã tồn tại → luồng đổi mật khẩu
                await _otpRepo.InvalidateAllByTaiKhoanAsync(taiKhoan.TaiKhoanID);
                var otp = new Otp(taiKhoan.TaiKhoanID, maOtp);
                await _otpRepo.AddAsync(otp);
            }
            else
            {
                // Chưa có tài khoản → luồng đăng ký
                await _otpRepo.InvalidateAllByEmailAsync(email);
                var otp = new Otp(email, maOtp);
                await _otpRepo.AddAsync(otp);
            }

            await _emailService.SendOtpAsync(email, maOtp);
            return ApiResponse<bool>.SuccessResponse(true, "OTP đã được gửi đến email của bạn");
        }
        catch (ArgumentException ex)
        {
            return ApiResponse<bool>.Fail(ex.Message);
        }
    }

    public async Task<ApiResponse<bool>> XacThucOtpAsync(XacThucOtpRequestDTO dto)
    {
        try
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Email)
                            || string.IsNullOrWhiteSpace(dto.MaOTP))
                return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

            var email = dto.Email.Trim().ToLower();
            var taiKhoan = await _taiKhoanRepo.GetByEmailAsync(email);

            Otp? otp;
            if (taiKhoan != null)
                otp = await _otpRepo.GetValidOtpByTaiKhoanAsync(taiKhoan.TaiKhoanID, dto.MaOTP.Trim());
            else
                otp = await _otpRepo.GetValidOtpByEmailAsync(email, dto.MaOTP.Trim());

            if (otp == null || !otp.IsValid())
                return ApiResponse<bool>.Fail("Mã OTP không hợp lệ hoặc đã hết hạn");

            otp.Invalidate();
            await _otpRepo.InvalidateAsync(otp.OtpID);
            return ApiResponse<bool>.SuccessResponse(true, "Xác thực OTP thành công");
        }
        catch (InvalidOperationException ex)
        {
            return ApiResponse<bool>.Fail(ex.Message);
        }
    }

    private static string TaoMaOtp() => Random.Shared.Next(100000, 999999).ToString();
}