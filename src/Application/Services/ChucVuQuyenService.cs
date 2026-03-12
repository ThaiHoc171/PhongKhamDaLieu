using Application.Common;
using Application.DTOs;
using Application.Interfaces;

namespace Application.Services;

public class ChucVuQuyenService
{
	private readonly IChucVuQuyenRepository _repo;
	private readonly IQuyenRepository _quyenRepo;

	public ChucVuQuyenService(
		IChucVuQuyenRepository repo,
		IQuyenRepository quyenRepo)
	{
		_repo = repo;
		_quyenRepo = quyenRepo;
	}

	public async Task<ApiResponse<List<QuyenChecklistDTO>>> GetChecklistAsync(int chucVuId)
	{
		var allQuyen = await _quyenRepo.GetAllAsync();
		var selected = await _repo.GetByChucVuAsync(chucVuId);

		var result = allQuyen.Select(q => new QuyenChecklistDTO
		{
			QuyenID = q.Id,
			TenQuyen = q.Name,
			Checked = selected.Contains(q.Id)
		}).ToList();

		return ApiResponse<List<QuyenChecklistDTO>>.SuccessResponse(result);
	}

	public async Task<ApiResponse<bool>> UpdateAsync(ChucVuQuyenDTO dto)
	{
		if (dto.QuyenIDs == null)
			return ApiResponse<bool>.Fail("Danh sách quyền không hợp lệ");

		await _repo.DeleteAllAsync(dto.ChucVuID);

		foreach (var id in dto.QuyenIDs)
			await _repo.AddAsync(dto.ChucVuID, id);

		return ApiResponse<bool>.SuccessResponse(true, "Cập nhật quyền thành công");
	}

	public async Task<ApiResponse<bool>> AddAsync(int chucVuId, int quyenId)
	{
		await _repo.AddAsync(chucVuId, quyenId);

		return ApiResponse<bool>.SuccessResponse(true, "Thêm quyền thành công");
	}

	public async Task<ApiResponse<bool>> DeleteAsync(int chucVuId, int quyenId)
	{
		await _repo.DeleteAsync(chucVuId, quyenId);

		return ApiResponse<bool>.SuccessResponse(true, "Xóa quyền thành công");
	}
}