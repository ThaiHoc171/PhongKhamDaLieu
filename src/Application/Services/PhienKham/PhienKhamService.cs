using Application.Common;
using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Application.Services;

public class PhienKhamService
{
	private readonly IPhienKhamRepository _repo;
	private readonly ICaKhamRepository _caKhamRepo;
	private readonly IPhienKhamBenhRepository _pkBenhrepo;
	private readonly IPhienKhamCLSRepository _pkClsRepo;
	private readonly ILichLamViecRepository _lichRepo;
	private readonly IBenhNhanRepository _benhNhanRepo;
	private readonly IHoSoBenhAnRepository _hoSoBenhAnRepo;
	private readonly INhanVienRepository _nhanVienRepo;

	public PhienKhamService(
		IPhienKhamRepository repo,
		IPhienKhamBenhRepository pkBenhrepo,
		IPhienKhamCLSRepository pkClsRepo,
		IBenhNhanRepository benhNhanRepo,
		IHoSoBenhAnRepository hoSoBenhAnRepo,
		ICaKhamRepository caKhamRepo,
		ILichLamViecRepository lichRepo,
		INhanVienRepository nhanVienRepo)
	{
		_repo = repo;
		_pkBenhrepo = pkBenhrepo;
		_pkClsRepo = pkClsRepo;
		_benhNhanRepo = benhNhanRepo;
		_hoSoBenhAnRepo = hoSoBenhAnRepo;
		_caKhamRepo = caKhamRepo;
		_lichRepo = lichRepo;
		_nhanVienRepo = nhanVienRepo;
	}

	public async Task<ApiResponse<int>> CreateAsync(int caKhamID)
	{
		try
		{
			if (caKhamID <= 0)
				return ApiResponse<int>.Fail("ID ca khám không hợp lệ");

			var caKham = await _caKhamRepo.GetByIdAsync(caKhamID);

			if (caKham == null || caKham.LichLamViecID == null || caKham.ThongTinID == null)
				return ApiResponse<int>.Fail("Ca khám không tồn tại");

			if (caKham.TrangThai != "Đã xác nhận")
				return ApiResponse<int>.Fail("Ca khám chưa được xác nhận hoặc đã kết thúc");

			var lich = await _lichRepo.GetByIdAsync(caKham.LichLamViecID.Value);

			if (lich == null)
				return ApiResponse<int>.Fail("Không tìm thấy lịch làm việc");

			var nv = await _nhanVienRepo.GetByIdAsync(lich.NhanVienID);

			if (nv == null)
				return ApiResponse<int>.Fail("Không tìm thấy nhân viên");

			var bn = await _benhNhanRepo.GetDetailAsync(caKham.ThongTinID.Value);

			if (bn == null)
				return ApiResponse<int>.Fail("Bệnh nhân không tồn tại");

			var entity = new PhienKham(
				caKhamID,
				bn.BenhNhanID,
				nv.NhanVienID,
				nv.PhongChucNangID
			);

			var id = await _repo.AddAsync(entity);

			if (id <= 0)
				return ApiResponse<int>.Fail("Tạo phiên khám thất bại");

			return ApiResponse<int>.SuccessResponse(id, "Tạo phiên khám thành công");
		}
		catch (Exception)
		{
			return ApiResponse<int>.Fail("Có lỗi xảy ra khi tạo phiên khám");
		}
	}
	public async Task<ApiResponse<bool>> UpdateAsync(int id, PhienKhamUpdateDTO dto)
	{
		try
		{
			if (id <= 0)
				return ApiResponse<bool>.Fail("ID không hợp lệ");

			if (dto == null)
				return ApiResponse<bool>.Fail("Dữ liệu không hợp lệ");

			var pk = await _repo.GetByIdAsync(id);

			if (pk == null)
				return ApiResponse<bool>.Fail("Phiên khám không tồn tại");

			pk.CapNhat(dto.TrieuChung, dto.GhiChu, dto.HinhAnh);

			var row = await _repo.UpdateAsync(pk);

			if (row == 0)
				return ApiResponse<bool>.Fail("Cập nhật thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Cập nhật thành công");
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (Exception)
		{
			return ApiResponse<bool>.Fail("Có lỗi xảy ra khi cập nhật phiên khám");
		}
	}

	public async Task<ApiResponse<bool>> CompleteAsync(int phienKhamId, string chanDoanCuoi)
	{
		try
		{
			if (phienKhamId <= 0)
				return ApiResponse<bool>.Fail("ID phiên khám không hợp lệ");

			if (string.IsNullOrWhiteSpace(chanDoanCuoi))
				return ApiResponse<bool>.Fail("Chẩn đoán cuối không hợp lệ");

			var pk = await _repo.GetByIdAsync(phienKhamId);

			if (pk == null)
				return ApiResponse<bool>.Fail("Phiên khám không tồn tại");

			if (!string.IsNullOrEmpty(pk.ChanDoanCuoi))
				return ApiResponse<bool>.Fail("Phiên khám đã kết thúc trước đó");

			var pkBenh = await _pkBenhrepo.GetByPhienKhamIdAsync(phienKhamId);

			if (pkBenh == null || !pkBenh.Any())
				return ApiResponse<bool>.Fail("Phải có ít nhất một chẩn đoán bệnh");

			var pkCls = await _pkClsRepo.GetByPhienKhamAsync(phienKhamId);

			if (pkCls != null &&
				pkCls.Any(c => TrangThaiCLSExtensions.ToEnum(c.TrangThai) != TrangThaiCLSEnum.HoanThanh))
			{
				return ApiResponse<bool>.Fail("Tất cả CLS phải hoàn thành trước khi kết thúc");
			}

			var hs = await _hoSoBenhAnRepo.GetByBenhNhanIdAsync(pk.BenhNhanID);

			if (hs == null)
				return ApiResponse<bool>.Fail("Chưa có hồ sơ bệnh án");

			pk.KetThuc(chanDoanCuoi);

			var row = await _repo.KetThucAsync(pk);

			if (row == 0)
				return ApiResponse<bool>.Fail("Kết thúc phiên khám thất bại");

			return ApiResponse<bool>.SuccessResponse(true, "Kết thúc phiên khám thành công");
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		catch (Exception)
		{
			return ApiResponse<bool>.Fail("Có lỗi xảy ra khi kết thúc phiên khám");
		}
	}

	public async Task<ApiResponse<PhienKhamReadModel>> GetByIdAsync(int id)
	{
		if (id <= 0)
			return ApiResponse<PhienKhamReadModel>.Fail("ID không hợp lệ");

		var result = await _repo.GetDetailAsync(id);

		if (result == null)
			return ApiResponse<PhienKhamReadModel>.Fail("Phiên khám không tồn tại");

		return ApiResponse<PhienKhamReadModel>.SuccessResponse(result);
	}

	public async Task<ApiResponse<PhienKhamReadModel>> GetByCaKhamIdAsync(int caKhamId)
	{
		if (caKhamId <= 0)
			return ApiResponse<PhienKhamReadModel>.Fail("ID ca khám không hợp lệ");

		var result = await _repo.GetByCaKhamIdAsync(caKhamId);

		if (result == null)
			return ApiResponse<PhienKhamReadModel>.Fail("Phiên khám không tồn tại");

		return ApiResponse<PhienKhamReadModel>.SuccessResponse(result);
	}

	// ================= PAGED =================

	public async Task<ApiResponse<PagedResult<PhienKhamReadListModel>>> GetPagedAsync(
		int pageNumber,
		int pageSize,
		int? nhanVienID,
		string? trangThai)
	{
		if (pageNumber < 1) pageNumber = 1;
		if (pageSize <= 0) pageSize = 10;

		var (items, totalCount) =
			await _repo.GetPagedAsync(pageNumber, pageSize, nhanVienID, trangThai);

		return ApiResponse<PagedResult<PhienKhamReadListModel>>.SuccessResponse(
			new PagedResult<PhienKhamReadListModel>
			{
				Items = items,
				TotalCount = totalCount,
				PageNumber = pageNumber,
				PageSize = pageSize
			});
	}

	public async Task<ApiResponse<PagedResult<PhienKhamReadListModel>>> SearchAsync(
		string keyword,
		int pageNumber,
		int pageSize,
		int? nhanVienID)
	{
		if (string.IsNullOrWhiteSpace(keyword))
			return ApiResponse<PagedResult<PhienKhamReadListModel>>.Fail("Từ khóa không hợp lệ");

		if (pageNumber < 1) pageNumber = 1;
		if (pageSize <= 0) pageSize = 10;

		var (items, totalCount) =
			await _repo.SearchPagedAsync(keyword.Trim(), pageNumber, pageSize, nhanVienID);

		return ApiResponse<PagedResult<PhienKhamReadListModel>>.SuccessResponse(
			new PagedResult<PhienKhamReadListModel>
			{
				Items = items,
				TotalCount = totalCount,
				PageNumber = pageNumber,
				PageSize = pageSize
			});
	}

	public async Task<ApiResponse<PagedResult<PhienKhamReadListModel>>> GetByBenhNhanAsync(
		int benhNhanId,
		int pageNumber,
		int pageSize)
	{
		if (benhNhanId <= 0)
			return ApiResponse<PagedResult<PhienKhamReadListModel>>.Fail("ID bệnh nhân không hợp lệ");

		if (pageNumber < 1) pageNumber = 1;
		if (pageSize <= 0) pageSize = 10;

		var (items, totalCount) =
			await _repo.GetBenhNhanPagedAsync(benhNhanId, pageNumber, pageSize);

		return ApiResponse<PagedResult<PhienKhamReadListModel>>.SuccessResponse(
			new PagedResult<PhienKhamReadListModel>
			{
				Items = items,
				TotalCount = totalCount,
				PageNumber = pageNumber,
				PageSize = pageSize
			});
	}
}