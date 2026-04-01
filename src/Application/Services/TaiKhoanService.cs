using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Application.Services;

public class TaiKhoanService
{
	private readonly ITaiKhoanRepository _repo;
	private readonly IConfiguration _config;

	public TaiKhoanService(
		ITaiKhoanRepository repo,
		IConfiguration config)
	{
		_repo = repo;
		_config = config;
	}

	public async Task<ApiResponse<int>> CreateAsync(TaiKhoanRequestDTO dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<int>.Fail("Dữ liệu không hợp lệ");

			var hash = Helper.Password.PassWordHash(dto.MatKhau);

			var entity = new TaiKhoan(
				dto.Email.Trim(),
				hash,
				VaiTroExtensions.ToEnum(dto.VaiTro));

			var id = await _repo.AddAsync(entity);

			return ApiResponse<int>.SuccessResponse(id, "Tạo tài khoản thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<int>.Fail(ex.Message);
		}
		catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<int>.Fail("Email đã tồn tại");
		}
	}

	public async Task<ApiResponse<bool>> ChangePasswordAsync(
		int id,
		ChangePasswordRequestDTO dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var tk = await _repo.GetByIdAsync(id);

			if (tk == null)
				return ApiResponse<bool>.Fail("Tài khoản không tồn tại");

			if (!Helper.Password.VerifyPassword(dto.MatKhauCu, tk.MatKhau))
				return ApiResponse<bool>.Fail("Mật khẩu cũ không đúng");

			var hash = Helper.Password.PassWordHash(dto.MatKhauMoi);

			tk.ChangePassword(hash);

			await _repo.UpdateAsync(tk);

			return ApiResponse<bool>.SuccessResponse(true, "Đổi mật khẩu thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}

	public async Task<ApiResponse<bool>> ResetPasswordAsync(int id)
	{
		try
		{
			var tk = await _repo.GetByIdAsync(id);

			if (tk == null)
				return ApiResponse<bool>.Fail("Tài khoản không tồn tại");

			var defaultPassword = _config["DefaultPassword"];

			if (string.IsNullOrWhiteSpace(defaultPassword))
				return ApiResponse<bool>.Fail("Chưa cấu hình mật khẩu mặc định");

			var hash = Helper.Password.PassWordHash(defaultPassword);

			tk.ChangePassword(hash);

			await _repo.UpdateAsync(tk);

			return ApiResponse<bool>.SuccessResponse(true, "Reset mật khẩu thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}

	public async Task<ApiResponse<bool>> UpdateStatusAsync(
		int id,
		TaiKhoanUpdateRequestDTO dto)
	{
		try
		{
			var tk = await _repo.GetByIdAsync(id);

			if (tk == null)
				return ApiResponse<bool>.Fail("Tài khoản không tồn tại");
			if(dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");
			if (dto.TrangThai == "Bị khóa")
				tk.Lock();

			if (dto.TrangThai == "Hoạt động")
				tk.Unlock();

			await _repo.UpdateAsync(tk);

			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật trạng thái thành công");
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}

	public async Task<ApiResponse<PagedResult<TaiKhoanListReadModel>>> 
		GetPagedAsync(int page, int size, string? vaiTro, string? trangThai)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.GetPagedAsync(page, size, vaiTro, trangThai);

		var result = new PagedResult<TaiKhoanListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<TaiKhoanListReadModel>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<TaiKhoanListReadModel>>> SearchAsync(int page, int size, string keyword, string? vaiTro, string? trangThai)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<TaiKhoanListReadModel>>
				.Fail("Từ khóa không hợp lệ");

		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _repo.SearchPagedAsync( page, size,keyword.Trim(),vaiTro,trangThai);

		var result = new PagedResult<TaiKhoanListReadModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};

		return ApiResponse<PagedResult<TaiKhoanListReadModel>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<TaiKhoanReadModel>> GetDetailAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<TaiKhoanReadModel>.Fail("ID không hợp lệ");

		var result = await _repo.GetDetailAsync(id);

		if (result == null)
			return ApiResponse<TaiKhoanReadModel>.Fail("Tài khoản không tồn tại");

		return ApiResponse<TaiKhoanReadModel>.SuccessResponse(result);
	}

	public async Task<ApiResponse<bool>> UpdateFcmTokenAsync(int taiKhoanId, string? token)
	{
		var tk = await _repo.GetByIdAsync(taiKhoanId);

		if (tk == null)
			return ApiResponse<bool>.Fail("Tài khoản không tồn tại");

		await _repo.UpdateFcmTokenAsync(taiKhoanId, token);

		return ApiResponse<bool>.SuccessResponse(true);
	}
}