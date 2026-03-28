using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Extensions.Configuration;
namespace Application.Services;
public class TaiKhoanService
{
	private readonly ITaiKhoanRepository _repo;
	private readonly IConfiguration _configuration;
	public TaiKhoanService(
		ITaiKhoanRepository repo,
		IConfiguration configuration)
	{
		_repo = repo;
		_configuration = configuration;
	}
	public async Task<ApiResponse<int>> CreateAsync(TaiKhoanRequestDTO dto)
	{
		if (string.IsNullOrWhiteSpace(dto.Email))
			return ApiResponse<int>.Fail("Email không hợp lệ");
		if (string.IsNullOrWhiteSpace(dto.MatKhau))
			return ApiResponse<int>.Fail("Mật khẩu không hợp lệ");
        var isExist = await _repo.ExistsByEmailAsync(dto.Email);
        if (isExist)
            return ApiResponse<int>.Fail("Email đã tồn tại");
        var hash = Helper.Password.PassWordHash(dto.MatKhau);
		var vaiTro = VaiTroExtensions.ToEnum(dto.VaiTro);
		var entity = new TaiKhoan(dto.Email, hash, vaiTro);
		var id = await _repo.AddAsync(entity);
		return ApiResponse<int>.SuccessResponse(id, "Tạo tài khoản thành công");
	}
	public async Task<ApiResponse<bool>> ChangePasswordAsync(
		int id,
		ChangePasswordRequestDTO dto)
	{
		var tk = await _repo.GetByIdAsync(id);
		if (tk == null)
			return ApiResponse<bool>.Fail("Tài khoản không tồn tại");
		if (!Helper.Password.VerifyPassword(dto.MatKhauCu, tk.MatKhau))
			return ApiResponse<bool>.Fail("Mật khẩu cũ không đúng");
		tk.ChangePassword(Helper.Password.PassWordHash(dto.MatKhauMoi));
		await _repo.UpdateAsync(tk);
		return ApiResponse<bool>.SuccessResponse(true, "Đổi mật khẩu thành công");
	}
	public async Task<ApiResponse<bool>> ResetPasswordAsync(int taiKhoanId)
	{
		var tk = await _repo.GetByIdAsync(taiKhoanId);
		if (tk == null)
			return ApiResponse<bool>.Fail("Tài khoản không tồn tại");
		var defaultPassword = _configuration["DefaultPassword"];
		if (string.IsNullOrWhiteSpace(defaultPassword))
			return ApiResponse<bool>.Fail("Chưa cấu hình mật khẩu mặc định");
		var hash = Helper.Password.PassWordHash(defaultPassword);
		tk.ChangePassword(hash);
		await _repo.UpdateAsync(tk);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<PagedResult<TaiKhoanListReadModel>>> GetPagedAsync(
		int page,
		int size,
		string? vaiTro,
		string? trangThai)
	{
		var (items, total) =
			await _repo.GetPagedAsync(page, size, vaiTro, trangThai);
		return ApiResponse<PagedResult<TaiKhoanListReadModel>>.SuccessResponse(
			new PagedResult<TaiKhoanListReadModel>
			{
				Items = items,
				TotalCount = total,
				PageNumber = page,
				PageSize = size
			});
	}
	public async Task<ApiResponse<TaiKhoanReadModel>> GetByIdAsync(int id)
	{
		var result = await _repo.GetDetailAsync(id);
		if (result == null)
			return ApiResponse<TaiKhoanReadModel>.Fail("Tài khoản không tồn tại");
		return ApiResponse<TaiKhoanReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<bool>> UpdateStatusAsync(
		int id,
		TaiKhoanUpdateRequestDTO dto)
	{
		var tk = await _repo.GetByIdAsync(id);
		if (tk == null)
			return ApiResponse<bool>.Fail("Tài khoản không tồn tại");
		try
		{
			if (dto.TrangThai == "Bị khóa")
				tk.Lock();
			else if (dto.TrangThai == "Hoạt động")
				tk.Unlock();
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		await _repo.UpdateAsync(tk);
		return ApiResponse<bool>.SuccessResponse(true, "Thay đổi trạng thái tài khoản thành công");
	}
    public async Task<ApiResponse<bool>> UpdateFcmTokenAsync(
        int taiKhoanId,
        string? fcmToken)
    {
        var tk = await _repo.GetByIdAsync(taiKhoanId);
        if (tk == null)
            return ApiResponse<bool>.Fail("Tài khoản không tồn tại");

        await _repo.UpdateFcmTokenAsync(taiKhoanId, fcmToken);
        return ApiResponse<bool>.SuccessResponse(true, "Cập nhật FCM token thành công");
    }
}