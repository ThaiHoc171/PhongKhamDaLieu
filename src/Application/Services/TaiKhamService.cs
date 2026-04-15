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

	// ==================== CREATE ====================
	public async Task<ApiResponse<int>> AddAsync(TaiKhamRequestDTO dto)
	{
		try
		{
			if (dto == null)
				return ApiResponse<int>.Fail("Dữ liệu không hợp lệ");

			if (dto.PhienKhamID <= 0)
				return ApiResponse<int>.Fail("PhienKhamID không hợp lệ");

			if (dto.NgayDuKien.Date <= DateTime.Today)
				return ApiResponse<int>.Fail("Ngày tái khám không hợp lệ");

			var benhNhanId = await _phienKhamRepo.GetBenhNhanByIdAsync(dto.PhienKhamID);
			if (benhNhanId == null)
				return ApiResponse<int>.Fail("Phiên khám không tồn tại");

			var res = await _taiKhamRepo.GetTaiKhamDangChoAsync(benhNhanId.Value);

			if (res != null &&
				res.TrangThai == TaiKhamEnum.ChoKham)
			{
				return ApiResponse<int>.Fail("Bệnh nhân còn lịch tái khám chưa xử lý");
			}

			var exists =
				await _taiKhamRepo.ExistsByPhienKhamAsync(dto.PhienKhamID);

			if (exists)
				return ApiResponse<int>.Fail("Phiên khám này đã có tái khám");

			var entity = new TaiKham(
				dto.PhienKhamID,
				benhNhanId.Value,
				dto.NgayDuKien,
				dto.LyDo
			);

			var id = await _taiKhamRepo.AddAsync(entity);

			if (id <= 0)
				return ApiResponse<int>.Fail("Tạo tái khám thất bại");

			return ApiResponse<int>.SuccessResponse(id, "Tạo tái khám thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<int>.Fail(ex.Message);
		}
	}

	// ==================== UPDATE ====================
	public async Task<ApiResponse<bool>> UpdateAsync(int id, TaiKhamUpdateRequestDTO dto)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

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

			var row = await _taiKhamRepo.UpdateAsync(taiKham);

			if (row == 0)
				return ApiResponse<bool>.Fail("Cập nhật thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}

	// ==================== UPDATE STATUS ====================
	public async Task<ApiResponse<bool>> CompleteAsync(int id)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			var res = await _taiKhamRepo.GetByIdAsync(id);
			if (res == null)
				return ApiResponse<bool>.Fail("Tái khám không tồn tại");

			res.Complete();

			var row = await _taiKhamRepo.UpdateAsync(res);

			if (row == 0)
				return ApiResponse<bool>.Fail("Cập nhật thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Hoàn thành tái khám");
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}
	public async Task<ApiResponse<bool>> CancelAsync(int id)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			var res = await _taiKhamRepo.GetByIdAsync(id);
			if (res == null)
				return ApiResponse<bool>.Fail("Tái khám không tồn tại");

			res.Cancel();

			var row = await _taiKhamRepo.UpdateAsync(res);

			if (row == 0)
				return ApiResponse<bool>.Fail("Hủy thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Hủy tái khám thành công");
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}
	// ==================== ASSIGN CA KHAM ====================
	public async Task<ApiResponse<bool>> GanCaKhamAsync(int taiKhamId, int caKhamId)
	{
		try
		{
			if (taiKhamId <= 0 || caKhamId <= 0)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var res = await _taiKhamRepo.GetByIdAsync(taiKhamId);
			if (res == null)
				return ApiResponse<bool>.Fail("Tái khám không tồn tại");

			if (res.TrangThai == TaiKhamEnum.DaKham)
				return ApiResponse<bool>.Fail("Tái khám đã hoàn thành");

			if (res.CaKhamID != null)
				return ApiResponse<bool>.Fail("Đã có ca khám");

			var caKham = await _caKhamRepo.GetByIdAsync(caKhamId);
			if (caKham == null)
				return ApiResponse<bool>.Fail("Ca khám không tồn tại");

			res.CapNhatCaKham(caKhamId);

			var row = await _taiKhamRepo.UpdateAsync(res);

			if (row == 0)
				return ApiResponse<bool>.Fail("Gán ca khám thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Gán ca khám thành công");
		}
		catch (ArgumentException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
	}

	// ==================== GET DETAIL ====================
	public async Task<ApiResponse<TaiKhamReadModel>> GetDetailAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<TaiKhamReadModel>.Fail("ID không hợp lệ");

		var res = await _taiKhamRepo.GetDetailAsync(id);

		if (res == null)
			return ApiResponse<TaiKhamReadModel>.Fail("Tái khám không tồn tại");

		return ApiResponse<TaiKhamReadModel>.SuccessResponse(res);
	}

	// ==================== GET PAGED ====================
	public async Task<ApiResponse<PagedResult<TaiKhamReadListModel>>> GetPagedAsync(
		int page,
		int size,
		string? trangThai)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _taiKhamRepo.GetPagedAsync(page, size, trangThai);

		return ApiResponse<PagedResult<TaiKhamReadListModel>>.SuccessResponse(
			new PagedResult<TaiKhamReadListModel>
			{
				Items = items,
				TotalCount = total,
				PageNumber = page,
				PageSize = size
			});
	}

	// ==================== SEARCH ====================
	public async Task<ApiResponse<PagedResult<TaiKhamReadListModel>>> SearchAsync(
		string? keyword,
		int page,
		int size)
	{
		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) = await _taiKhamRepo.SearchAsync(keyword, page, size);

		return ApiResponse<PagedResult<TaiKhamReadListModel>>.SuccessResponse(
			new PagedResult<TaiKhamReadListModel>
			{
				Items = items,
				TotalCount = total,
				PageNumber = page,
				PageSize = size
			});
	}

	// ==================== GET BY BENH NHAN ====================
	public async Task<ApiResponse<PagedResult<TaiKhamReadListModel>>> GetByBenhNhanAsync(
		int benhNhanId,
		int page,
		int size)
	{
		if (benhNhanId <= 0)
			return ApiResponse<PagedResult<TaiKhamReadListModel>>.Fail("ID không hợp lệ");

		if (page < 1) page = 1;
		if (size <= 0) size = 10;

		var (items, total) =
			await _taiKhamRepo.GetPagedByBenhNhanAsync(benhNhanId, page, size);

		return ApiResponse<PagedResult<TaiKhamReadListModel>>.SuccessResponse(
			new PagedResult<TaiKhamReadListModel>
			{
				Items = items,
				TotalCount = total,
				PageNumber = page,
				PageSize = size
			});
	}
}