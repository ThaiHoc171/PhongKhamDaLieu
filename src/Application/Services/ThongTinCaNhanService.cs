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

	public async Task<ApiResponse<bool>> AddKhachAsync(ThongTinRequestDTO dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");
			bool isExist = await _repo.ExistsByEmailAsync(dto.EmailLienHe, dto.SDT);
			if (isExist)
				return ApiResponse<bool>.Fail("Email hoặc số điện thoại đã tồn tại");

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

			int id = await _repo.AddAsync(entity);

			if (id <= 0)
				return ApiResponse<bool>.Fail("Tạo thông tin thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Tạo thông tin thành công");
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

	public async Task<ApiResponse<bool>> UpdateAsync(int id, ThongTinUpdateRequestDTO dto)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			if (dto.NgaySinh == null)
				return ApiResponse<bool>.Fail("Ngày sinh không hợp lệ");

			var entity = await _repo.GetByIdAsync(id);

			if (entity == null)
				return ApiResponse<bool>.Fail("Không tìm thấy thông tin");

			entity.CapNhat(
				dto.HoTen.Trim(),
				dto.NgaySinh.Value,
				GioiTinhExtensions.FromDbValue(dto.GioiTinh),
				dto.SDT,
				dto.EmailLienHe,
				dto.DiaChi,
				dto.Avatar,
				LoaiThongTinExtensions.FromDbValue(dto.Loai)
			);

			await _repo.UpdateAsync(entity);

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

	public async Task<ApiResponse<List<ThongTinReadListModel>>> DanhSachKhachAsync()
	{
		var list = await _repo.GetAllByLoaiAsync(LoaiThongTinEnum.BenhNhan);

		return ApiResponse<List<ThongTinReadListModel>>.SuccessResponse(list);
	}

	public async Task<ApiResponse<bool>> CapNhatTaiKhoanAsync(int thongTinId, int taiKhoanId)
	{
		try
		{
			if (thongTinId <= 0 || taiKhoanId <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			var entity = await _repo.GetByIdAsync(thongTinId);
			int reulst = await _repo.GetIdByTaiKhoanId(taiKhoanId);
			if (entity == null)
				return ApiResponse<bool>.Fail("Không tìm thấy thông tin");
			if (reulst != thongTinId && reulst != 0)
				return ApiResponse<bool>.Fail("Tài khoản đã được liên kết với thông tin khác");

			entity.CapNhatTaiKhoan(taiKhoanId);

			await _repo.UpdateAsync(entity);

			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật tài khoản thành công");
		}
		catch (Exception ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}
}