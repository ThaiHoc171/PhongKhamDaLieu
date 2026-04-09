using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Microsoft.Data.SqlClient;

namespace Application.Services;

public class ThongTinCaNhanService
{
	private readonly IThongTinCaNhanRepository _repo;

	public ThongTinCaNhanService(IThongTinCaNhanRepository repo)
	{
		_repo = repo;
	}

	public async Task<ApiResponse<int>> AddKhachAsync(ThongTinRequestDTO dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<int>.Fail("Dữ liệu không hợp lệ");

			var entity = new ThongTinCaNhan(
				dto.HoTen.Trim(),
				dto.NgaySinh,
				GioiTinhExtensions.FromDbValue(dto.GioiTinh),
				dto.SDT,
				dto.EmailLienHe,
				dto.DiaChi,
				dto.Avatar,
				LoaiThongTinEnum.Khach,
				dto.TaiKhoanID
			);

			int res = await _repo.AddAsync(entity);

			if (res == 0)
				return ApiResponse<int>.Fail("Tạo thông tin thất bại");

			return ApiResponse<int>.SuccessResponse(res, "Tạo thông tin thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<int>.Fail(ex.Message);
		}
		catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<int>.Fail("Email hoặc số điện thoại đã tồn tại");
		}
	}

	public async Task<ApiResponse<bool>> UpdateAsync(int id, ThongTinUpdateRequestDTO dto)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var entity = await _repo.GetByIdAsync(id);

			if (entity == null)
				return ApiResponse<bool>.Fail("Không tìm thấy thông tin");

			entity.CapNhat(
				dto.HoTen.Trim(),
				dto.NgaySinh,
				GioiTinhExtensions.FromDbValue(dto.GioiTinh),
				dto.SDT,
				dto.EmailLienHe,
				dto.DiaChi,
				dto.Avatar,
				LoaiThongTinExtensions.FromDbValue(dto.Loai)
			);

			int row = await _repo.UpdateAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Cập nhật thông tin thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật thông tin thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (SqlException ex) when (ex.Number == 2627 || ex.Number == 2601)
		{
			return ApiResponse<bool>.Fail("Email hoặc số điện thoại đã tồn tại");
		}
	}

	public async Task<ApiResponse<ThongTinReadModel>> GetDetailAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<ThongTinReadModel>.Fail("ID không hợp lệ");

		var result = await _repo.GetDetailAsync(id);

		if (result == null)
			return ApiResponse<ThongTinReadModel>.Fail("Thông tin không tồn tại");

		return ApiResponse<ThongTinReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<ThongTinReadListModel>>> GetPagedAsync(int page, int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;
		var (items, total) = await _repo.GetPagedAsync(page, size);
		var result = new PagedResult<ThongTinReadListModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
		return ApiResponse<PagedResult<ThongTinReadListModel>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<ThongTinReadListModel>>> SearchAsync(string keyword, int page, int size)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<ThongTinReadListModel>>
				.Fail("Từ khóa không hợp lệ");
		if (page < 1) page = 1;
		if (size <= 0) size = 10;
		var (items, total) = await _repo.SearchPagedAsync(keyword.Trim(), page, size);
		var result = new PagedResult<ThongTinReadListModel>
		{
			Items = items,
			TotalCount = total,
			PageNumber = page,
			PageSize = size
		};
		return ApiResponse<PagedResult<ThongTinReadListModel>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<bool>> UpdateAccountAsync(int thongTinId, int taiKhoanId, string email)
	{
		try
		{
			if (thongTinId <= 0 || taiKhoanId <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			var entity = await _repo.GetByIdAsync(thongTinId);

			if (entity == null)
				return ApiResponse<bool>.Fail("Không tìm thấy thông tin");

			int existId = await _repo.GetIdByTaiKhoanId(taiKhoanId);

			if (existId != 0 && existId != thongTinId)
				return ApiResponse<bool>.Fail("Tài khoản đã liên kết với thông tin khác");

			string? emailLienHe = entity.EmailLienHe;
			if (string.IsNullOrWhiteSpace(emailLienHe))
				emailLienHe = email;
			entity.CapNhatTaiKhoan(taiKhoanId,emailLienHe);

			int row = await _repo.UpdateAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Cập nhật tài khoản thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật tài khoản thành công");
		}
		catch (Exception ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}
}