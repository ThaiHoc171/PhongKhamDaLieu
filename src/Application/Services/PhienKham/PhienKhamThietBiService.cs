using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class PhienKhamThietBiService
{
	private readonly IPhienKhamThietBiRepository _repo;
	private readonly IPhienKhamRepository _phienKhamRepo;
	public PhienKhamThietBiService(IPhienKhamThietBiRepository repo, IPhienKhamRepository phienKhamRepo)
	{
		_repo = repo;
		_phienKhamRepo = phienKhamRepo;
	}

	public async Task<ApiResponse<List<PhienKhamThietBiReadModel>>> GetByPhienKhamAsync(int phienKhamID)
	{
		if (phienKhamID <= 0)
			return ApiResponse<List<PhienKhamThietBiReadModel>>
				.Fail("ID phiên khám không hợp lệ");

		var data = await _repo.GetByPhienKhamAsync(phienKhamID);

		return ApiResponse<List<PhienKhamThietBiReadModel>>
			.SuccessResponse(data);
	}

	public async Task<ApiResponse<bool>> AddAsync(PhienKhamThietBiRequestDTO dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			// Rule: 1 ChiTietID chỉ xuất hiện 1 lần trong 1 phiên khám
			var existed = await _repo.GetByPhienKhamAndChiTietAsync(
				dto.PhienKhamID,
				dto.ChiTietID
			);

			if (existed != null)
				return ApiResponse<bool>.Fail(
					"Thiết bị này đã được sử dụng trong phiên khám"
				);

			var entity = new PhienKhamThietBi(
				dto.PhienKhamID,
				dto.ChiTietID,
				dto.GhiChu
			);

			int row = await _repo.AddAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Thêm thiết bị vào phiên khám thất bại");

			return ApiResponse<bool>.SuccessResponse(
				true,
				"Thêm thiết bị vào phiên khám thành công"
			);
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}

	public async Task<ApiResponse<bool>> UpdateAsync(int id, string? ghiChu)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			var entity = await _repo.GetByIdAsync(id);

			if (entity == null)
				return ApiResponse<bool>.Fail("Không tìm thấy thiết bị trong phiên khám");
			var phienKham = await _phienKhamRepo.GetByIdAsync(entity.PhienKhamID);
			if (phienKham == null)
				return ApiResponse<bool>.Fail("Không tìm thấy phiên khám");
			if(phienKham.TrangThai != TrangThaiKhamEnum.DangKham)
				return ApiResponse<bool>.Fail("Phiên khám đã kết thúc");
			entity.CapNhatGhiChu(ghiChu);

			int row = await _repo.UpdateAsync(entity);

			if (row == 0)
				return ApiResponse<bool>.Fail("Cập nhật ghi chú thiết bị thất bại");

			return ApiResponse<bool>.SuccessResponse(
				true,
				"Cập nhật ghi chú thiết bị thành công"
			);
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
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
				return ApiResponse<bool>.Fail("Không tìm thấy thiết bị trong phiên khám");
			var phienKham = await _phienKhamRepo.GetByIdAsync(entity.PhienKhamID);
			if (phienKham == null)
				return ApiResponse<bool>.Fail("Không tìm thấy phiên khám");
			if (phienKham.TrangThai != TrangThaiKhamEnum.DangKham)
				return ApiResponse<bool>.Fail("Phiên khám đã kết thúc");
			int row = await _repo.DeleteAsync(id);
			if (row == 0)
				return ApiResponse<bool>.Fail("Xóa thiết bị khỏi phiên khám thất bại");
			return ApiResponse<bool>.SuccessResponse(
				true,
				"Xóa thiết bị khỏi phiên khám thành công"
			);
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}
}