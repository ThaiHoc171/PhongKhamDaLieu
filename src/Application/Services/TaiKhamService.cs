using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
namespace Application.Services;
public class TaiKhamService
{
	private readonly ITaiKhamRepository _taiKhamRepo;
	private readonly IPhienKhamRepository _phienKhamRepo;
	private readonly ICaKhamRepository _caKhamRepo;
	public TaiKhamService(
		ITaiKhamRepository taiKhamRepo,
		IPhienKhamRepository phienKhamRepo,
		ICaKhamRepository caKhamRepo)
	{
		_taiKhamRepo = taiKhamRepo;
		_phienKhamRepo = phienKhamRepo;
		_caKhamRepo = caKhamRepo;
	}
	public async Task<ApiResponse<int>> AddAsync(TaiKhamRequestDTO dto)
	{
		var validation = await ValidateCreate(dto);
		if (!validation.Success)
			return ApiResponse<int>.Fail(validation.Message!);
		var (phienKhamId, benhNhanId) = validation.Data!;
		var entity = new TaiKham(
			phienKhamId,
			benhNhanId,
			dto.NgayDuKien,
			dto.LyDo
		);
		var id = await _taiKhamRepo.AddAsync(entity);
		return ApiResponse<int>.SuccessResponse(id);
	}
	public async Task<ApiResponse<bool>> UpdateAsync(int id, TaiKhamUpdateRequestDTO dto)
	{
		var taiKham = await _taiKhamRepo.GetByIdAsync(id);
		if (taiKham == null)
			return ApiResponse<bool>.Fail("Tái khám không tồn tại");
		if (taiKham.TrangThai == TaiKhamEnum.DaKham)
			return ApiResponse<bool>.Fail("Tái khám đã hoàn thành");
		if (taiKham.CaKhamID != null &&
			dto.CaKhamID != taiKham.CaKhamID &&
			taiKham.TrangThai == TaiKhamEnum.ChoKham)
		{
			return ApiResponse<bool>.Fail("Không thể thay đổi ca khám khi đã có lịch");
		}
		if (taiKham.CaKhamID != null && dto.CaKhamID == null)
			return ApiResponse<bool>.Fail("Không thể hủy ca khám");
		taiKham.CapNhatCaKham(dto.CaKhamID);
		await _taiKhamRepo.UpdateAsync(taiKham);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<TaiKhamDetailReadModel>> GetDetailAsync(int id)
	{
		var result = await _taiKhamRepo.GetDetailAsync(id);
		if (result == null)
			return ApiResponse<TaiKhamDetailReadModel>.Fail("Tái khám không tồn tại");
		return ApiResponse<TaiKhamDetailReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<bool>> UpdateStatusAsync(int id, string trangThai)
	{
		var taiKham = await _taiKhamRepo.GetByIdAsync(id);
		if (taiKham == null)
			return ApiResponse<bool>.Fail("Tái khám không tồn tại");
		if (string.IsNullOrEmpty(trangThai))
			return ApiResponse<bool>.Fail("Trạng thái không hợp lệ");
		try
		{
			var status = TaiKhamExtensions.Parse(trangThai);
			if (taiKham.TrangThai == TaiKhamEnum.DaKham)
				return ApiResponse<bool>.Fail("Tái khám đã hoàn thành");
			taiKham.DoiTrangThai(status);
		}
		catch (Exception ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		await _taiKhamRepo.UpdateAsync(taiKham);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<PagedResult<TaiKhamReadModel>>> GetPagedAsync(
		int page,
		int size,
		string? trangThai)
	{
		var (items, total) = await _taiKhamRepo.GetPagedAsync(page, size, trangThai);
		return ApiResponse<PagedResult<TaiKhamReadModel>>.SuccessResponse(
			new PagedResult<TaiKhamReadModel>
			{
				Items = items,
				TotalCount = total,
				PageNumber = page,
				PageSize = size
			});
	}
	public async Task<ApiResponse<PagedResult<TaiKhamReadModel>>> SearchAsync(
		string? keyword,
		int page,
		int size)
	{
		var (items, total) = await _taiKhamRepo.SearchAsync(keyword, page, size);
		return ApiResponse<PagedResult<TaiKhamReadModel>>.SuccessResponse(
			new PagedResult<TaiKhamReadModel>
			{
				Items = items,
				TotalCount = total,
				PageNumber = page,
				PageSize = size
			});
	}
	public async Task<ApiResponse<PagedResult<TaiKhamReadModel>>> GetByBenhNhanAsync(
		int benhNhanId,
		int page,
		int size)
	{
		var (items, total) =
			await _taiKhamRepo.GetListByBenhNhanAsync(benhNhanId, page, size);
		return ApiResponse<PagedResult<TaiKhamReadModel>>.SuccessResponse(
			new PagedResult<TaiKhamReadModel>
			{
				Items = items,
				TotalCount = total,
				PageNumber = page,
				PageSize = size
			});
	}
	public async Task<ApiResponse<bool>> GanCaKhamAsync(int taiKhamId, int caKhamId)
	{
		var taiKham = await _taiKhamRepo.GetByIdAsync(taiKhamId);
		if (taiKham == null)
			return ApiResponse<bool>.Fail("Tái khám không tồn tại");
		if (taiKham.TrangThai == TaiKhamEnum.DaKham)
			return ApiResponse<bool>.Fail("Tái khám đã hoàn thành, không thể gán ca khám");
		if (taiKham.CaKhamID != null)
			return ApiResponse<bool>.Fail("Tái khám đã được gán ca khám trước đó");
		var caKham = await _caKhamRepo.GetByIdAsync(caKhamId);
		if (caKham == null)
			return ApiResponse<bool>.Fail("Ca khám không tồn tại");
		try
		{
			taiKham.CapNhatCaKham(caKhamId);
		}
		catch (Exception ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		await _taiKhamRepo.UpdateAsync(taiKham);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	private async Task<ApiResponse<(int PhienKhamID, int BenhNhanID)>> ValidateCreate(TaiKhamRequestDTO dto)
	{
		if (dto.PhienKhamID <= 0)
			return ApiResponse<(int, int)>.Fail("PhienKhamID không hợp lệ");
		if (dto.NgayDuKien.Date <= DateTime.Today)
			return ApiResponse<(int, int)>.Fail("Ngày tái khám không hợp lệ");
		var benhNhanId = await _phienKhamRepo.GetBenhNhanByIdAsync(dto.PhienKhamID);
		if (benhNhanId == null)
			return ApiResponse<(int, int)>.Fail("Phiên khám không tồn tại");
		var taiKhamDangCho =
			await _taiKhamRepo.GetTaiKhamDangChoAsync(benhNhanId.Value);
		if (taiKhamDangCho != null &&
			taiKhamDangCho.TrangThai == TaiKhamEnum.ChoKham)
		{
			return ApiResponse<(int, int)>.Fail(
				"Bệnh nhân còn lịch tái khám chưa xử lý");
		}
		var exists =
			await _taiKhamRepo.ExistsByPhienKhamAsync(dto.PhienKhamID);
		if (exists)
			return ApiResponse<(int, int)>.Fail(
				"Phiên khám này đã có tái khám");
		return ApiResponse<(int, int)>.SuccessResponse(
			(dto.PhienKhamID, benhNhanId.Value));
	}
}