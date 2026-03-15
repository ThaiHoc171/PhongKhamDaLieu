using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
namespace Application.Services;
public class LieuTrinhDieuTriService
{
	private readonly ILieuTrinhDieuTriRepository _repo;
	private readonly IPhienKhamRepository _phienKhamRepo;
	public LieuTrinhDieuTriService(
		ILieuTrinhDieuTriRepository repo,
		IPhienKhamRepository phienKhamRepo)
	{
		_repo = repo;
		_phienKhamRepo = phienKhamRepo;
	}
	public async Task<ApiResponse<int>> CreateAsync(LieuTrinhDieuTriRequestDTO dto)
	{
		if (dto.PhienKhamID <= 0)
			return ApiResponse<int>.Fail("Phiên khám không hợp lệ");
		if (string.IsNullOrWhiteSpace(dto.TenLieuTrinh))
			return ApiResponse<int>.Fail("Tên liệu trình không hợp lệ");
		if (dto.TongSoBuoi <= 0)
			return ApiResponse<int>.Fail("Tổng số buổi phải lớn hơn 0");
		var phienKham = await _phienKhamRepo.GetByIdAsync(dto.PhienKhamID);
		if (phienKham == null)
			return ApiResponse<int>.Fail("Phiên khám không tồn tại");
		var ngayKetThuc = dto.NgayBatDau.AddDays(dto.TongSoBuoi * 7);
		var entity = new LieuTrinhDieuTri(
			phienKham.BenhNhanID,
			dto.PhienKhamID,
			dto.TenLieuTrinh,
			dto.TongSoBuoi,
			dto.GhiChu,
			dto.NgayBatDau,
			ngayKetThuc
		);
		var id = await _repo.AddAsync(entity);
		return ApiResponse<int>.SuccessResponse(id);
	}
	public async Task<ApiResponse<bool>> UpdateAsync(int id, LieuTrinhDieuTriUpdateDTO dto)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Liệu trình không tồn tại");
		try
		{
			entity.Update(
				dto.TenLieuTrinh,
				dto.TongSoBuoi,
				dto.NgayKetThuc);
		}
		catch (Exception ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		await _repo.UpdateAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<bool>> CompleteAsync(int id)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Liệu trình không tồn tại");
		try
		{
			entity.Complete();
		}
		catch (Exception ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		await _repo.UpdateTrangThaiAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<bool>> CancelAsync(int id, string? ghiChu)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Liệu trình không tồn tại");
		try
		{
			entity.Cancel(ghiChu);
		}
		catch (Exception ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		await _repo.UpdateTrangThaiAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<bool>> UpdateStatusAsync(int id, string? ghiChu)
	{
		var entity = await _repo.GetByIdAsync(id);
		if (entity == null)
			return ApiResponse<bool>.Fail("Liệu trình không tồn tại");
		entity.Status(ghiChu);
		await _repo.UpdateTrangThaiAsync(entity);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<LieuTrinhDieuTriReadModel>> GetByIdAsync(int id)
	{
		var result = await _repo.GetDetailAsync(id);
		if (result == null)
			return ApiResponse<LieuTrinhDieuTriReadModel>.Fail("Liệu trình không tồn tại");
		return ApiResponse<LieuTrinhDieuTriReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<LieuTrinhDieuTriListReadModel>>> GetPagedAsync(
		int page,
		int size,
		string? trangThai)
	{
		var (items, totalCount) = await _repo.GetPagedAsync(page, size, trangThai);
		return ApiResponse<PagedResult<LieuTrinhDieuTriListReadModel>>.SuccessResponse(
			new PagedResult<LieuTrinhDieuTriListReadModel>
			{
				Items = items,
				TotalCount = totalCount,
				PageNumber = page,
				PageSize = size
			});
	}
	public async Task<ApiResponse<PagedResult<LieuTrinhDieuTriListReadModel>>> SearchAsync(
		string keyword,
		int page,
		int size)
	{
		var (items, totalCount) = await _repo.SearchAsync(keyword, page, size);
		return ApiResponse<PagedResult<LieuTrinhDieuTriListReadModel>>.SuccessResponse(
			new PagedResult<LieuTrinhDieuTriListReadModel>
			{
				Items = items,
				TotalCount = totalCount,
				PageNumber = page,
				PageSize = size
			});
	}
	public async Task<ApiResponse<PagedResult<LieuTrinhDieuTriListReadModel>>> GetByBenhNhanAsync(
		int benhNhanID,
		int page,
		int size)
	{
		var (items, totalCount) =
			await _repo.GetBenhNhanPagedAsync(benhNhanID, page, size);
		return ApiResponse<PagedResult<LieuTrinhDieuTriListReadModel>>.SuccessResponse(
			new PagedResult<LieuTrinhDieuTriListReadModel>
			{
				Items = items,
				TotalCount = totalCount,
				PageNumber = page,
				PageSize = size
			});
	}
}