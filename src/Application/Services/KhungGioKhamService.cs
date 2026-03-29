using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Data.SqlClient;

namespace Application.Services;

public class KhungGioKhamService
{
	private readonly IKhungGioKhamRepository _repo;

	public KhungGioKhamService(IKhungGioKhamRepository repo)
	{
		_repo = repo;
	}

	public async Task<ApiResponse<List<KhungGioKhamReadModel>>> GetAllAsync()
	{
		var list = await _repo.GetAllAsync();
		return ApiResponse<List<KhungGioKhamReadModel>>.SuccessResponse(list);
	}

	public async Task<ApiResponse<KhungGioKhamReadModel>> GetDetailAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<KhungGioKhamReadModel>.Fail("ID không hợp lệ");

		var result = await _repo.GetDetailAsync(id);

		if (result == null)
			return ApiResponse<KhungGioKhamReadModel>.Fail("Khung giờ khám không tồn tại");

		return ApiResponse<KhungGioKhamReadModel>.SuccessResponse(result);
	}

	public async Task<ApiResponse<bool>> AddAsync(KhungGioKhamRequest dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var entity = new KhungGioKham(
				dto.CaLamViec,
				dto.GioBatDau,
				dto.GioKetThuc
			);

			var danhSach = await _repo.GetAllAsync();

			if (danhSach.Any(x =>
				x.CaLamViec == dto.CaLamViec &&
				!(dto.GioKetThuc <= x.GioBatDau ||
				  dto.GioBatDau >= x.GioKetThuc)))
			{
				return ApiResponse<bool>.Fail("Khung giờ khám bị trùng trong cùng ca làm việc");
			}

			int row = await _repo.AddAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Tạo khung giờ khám thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Tạo khung giờ khám thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (SqlException)
		{
			return ApiResponse<bool>.Fail("Lỗi cơ sở dữ liệu khi tạo khung giờ khám");
		}
	}

	public async Task<ApiResponse<bool>> UpdateAsync(int id, KhungGioKhamRequest dto)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var entity = await _repo.GetByIdAsync(id);

			if (entity == null)
				return ApiResponse<bool>.Fail("Khung giờ khám không tồn tại");

			entity.CapNhat(
				dto.CaLamViec,
				dto.GioBatDau,
				dto.GioKetThuc
			);

			var danhSach = await _repo.GetAllAsync();

			if (danhSach.Any(x =>
				x.KhungGioID != id &&
				x.CaLamViec == dto.CaLamViec &&
				!(dto.GioKetThuc <= x.GioBatDau ||
				  dto.GioBatDau >= x.GioKetThuc)))
			{
				return ApiResponse<bool>.Fail("Khung giờ khám bị trùng trong cùng ca làm việc");
			}

			int row = await _repo.UpdateAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Cập nhật khung giờ khám thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật khung giờ khám thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (SqlException)
		{
			return ApiResponse<bool>.Fail("Lỗi cơ sở dữ liệu khi cập nhật");
		}
	}

	public async Task<ApiResponse<bool>> DeleteAsync(int id)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			var entity = await _repo.GetByIdAsync(id);

			if (entity == null)
				return ApiResponse<bool>.Fail("Không tìm thấy khung giờ");

			int row = await _repo.DeleteAsync(id);

			if (row == 0)
				return ApiResponse<bool>.Fail("Xóa khung giờ thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Xóa khung giờ thành công");
		}
		catch (SqlException)
		{
			return ApiResponse<bool>.Fail("Không thể xóa vì dữ liệu đang được sử dụng");
		}
	}

	public async Task<ApiResponse<List<NameResponseDTO>>> GetComboboxAsync()
	{
		var list = await _repo.GetComboboxAsync();
		return ApiResponse<List<NameResponseDTO>>.SuccessResponse(list);
	}



	// ĐỢI CHECK
	public async Task<ApiResponse<int>> CountAsync()
	{
		var total = await _repo.CountKhungGioKhamAsync();
		return ApiResponse<int>.SuccessResponse(total);
	}

	public async Task<ApiResponse<List<int>>> GetByCaLamViecAsync(int caLamViec)
	{
		var list = await _repo.GetKhungGioIdsByCaLamViecAsync(caLamViec);
		return ApiResponse<List<int>>.SuccessResponse(list);
	}
}