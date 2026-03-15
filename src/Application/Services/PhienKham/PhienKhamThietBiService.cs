using Application.DTOs;
using Application.Interfaces;
using Application.Common;
using Domain.Entities;
namespace Application.Services;
public class PhienKhamThietBiService
{
	private readonly IPhienKhamThietBiRepository _repo;
	public PhienKhamThietBiService(IPhienKhamThietBiRepository repo)
	{
		_repo = repo;
	}
	public async Task<ApiResponse<List<PhienKhamThietBiReadModel>>> DanhSachTheoPhienKhamAsync(int phienKhamID)
	{
		var data = await _repo.GetByPhienKhamAsync(phienKhamID);
		return ApiResponse<List<PhienKhamThietBiReadModel>>.SuccessResponse(
			data,
			"Lấy danh sách thiết bị thành công"
		);
	}
	public async Task<ApiResponse<object>> ThemMoiAsync(PhienKhamThietBiRequestDTO dto)
	{
		// Rule: 1 ChiTietID chỉ xuất hiện 1 lần trong 1 phiên khám
		var existed = await _repo.GetByPhienKhamAndChiTietAsync(
			dto.PhienKhamID,
			dto.ChiTietID
		);
		if (existed != null)
			return ApiResponse<object>.Fail(
				"Thiết bị này đã được sử dụng trong phiên khám"
			);
		var entity = new PhienKhamThietBi(
			dto.PhienKhamID,
			dto.ChiTietID,
			dto.GhiChu
		);
		await _repo.AddAsync(entity);
		return ApiResponse<object>.SuccessResponse(
			null,
			"Thêm thiết bị vào phiên khám thành công"
		);
	}
	public async Task<ApiResponse<object>> CapNhatAsync(int id, string? ghiChu)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<object>.Fail("Không tìm thấy thiết bị trong phiên khám");
		entity.CapNhatGhiChu(ghiChu);
		await _repo.UpdateAsync(entity);
		return ApiResponse<object>.SuccessResponse(
			null,
			"Cập nhật ghi chú thiết bị thành công"
		);
	}
}