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
			QuyenID = q.QuyenID,
			TenQuyen = q.TenQuyen,
			Module = q.Module,
			Checked = selected.Contains(q.QuyenID)
		}).ToList();
		return ApiResponse<List<QuyenChecklistDTO>>.SuccessResponse(result);
	}
	public async Task<ApiResponse<bool>> UpdateAsync(ChucVuQuyenDTO dto)
	{
		if (dto.QuyenIDs == null)
			return ApiResponse<bool>.Fail("Danh sách quyền không hợp lệ");

		var current = await _repo.GetByChucVuAsync(dto.ChucVuID);

		var toDelete = current.Except(dto.QuyenIDs).ToList();
		var toAdd = dto.QuyenIDs.Except(current).ToList();

		await _repo.DeleteRangeAsync(dto.ChucVuID, toDelete);
		await _repo.AddRangeAsync(dto.ChucVuID, toAdd);

		return ApiResponse<bool>.SuccessResponse(true, "Cập nhật quyền thành công");
	}
}