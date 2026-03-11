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
	public PhienKhamService(
		IPhienKhamRepository repo,
		IPhienKhamBenhRepository pkBenhrepo,
		IPhienKhamCLSRepository pkClsRepo,
		IBenhNhanRepository benhNhanRepo,
		IHoSoBenhAnRepository hoSoBenhAnRepo,
		ICaKhamRepository caKhamRepo,
		ILichLamViecRepository lichRepo)
	{
		_repo = repo;
		_pkBenhrepo = pkBenhrepo;
		_pkClsRepo = pkClsRepo;
		_benhNhanRepo = benhNhanRepo;
		_hoSoBenhAnRepo = hoSoBenhAnRepo;
		_caKhamRepo = caKhamRepo;
		_lichRepo = lichRepo;
	}
	public async Task<ApiResponse<int>> TaoMoiAsync(int caKhamID)
	{
		var caKham = await _caKhamRepo.GetByIdAsync(caKhamID);
		if (caKham == null)
			return ApiResponse<int>.Fail("Ca khám không tồn tại");
		if (caKham.TrangThai != "Đã xác nhận")
			return ApiResponse<int>.Fail("Ca khám chưa được xác nhận hoặc đã kết thúc");
		if (caKham.ThongTinID == null)
			return ApiResponse<int>.Fail("Không có ThongTinID");
		var nv = await _lichRepo.GetNhanVienById(caKham.LichLamViecID);
		if (nv.nhanvien == 0)
			return ApiResponse<int>.Fail("Không tìm thấy nhân viên cho ca khám");
		if (nv.phong == 0)
			return ApiResponse<int>.Fail("Không tìm thấy phòng chức năng");
		int? bn = await _benhNhanRepo.GetIdByThongTinAsync(caKham.ThongTinID.Value);
		if (bn == null)
			return ApiResponse<int>.Fail("Bệnh nhân không tồn tại");
		var entity = new PhienKham(
			caKhamID,
			bn.Value,
			nv.nhanvien,
			nv.phong);
		var id = await _repo.AddAsync(entity);
		return ApiResponse<int>.SuccessResponse(id);
	}
	public async Task<ApiResponse<bool>> CapNhatAsync(int id, PhienKhamUpdateDTO dto)
	{
		var pk = await _repo.GetByIdAsync(id);
		if (pk == null)
			return ApiResponse<bool>.Fail("Phiên khám không tồn tại");
		try
		{
			pk.CapNhat(dto.TrieuChung, dto.GhiChu, dto.HinhAnh);
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		await _repo.UpdateAsync(pk);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<bool>> KetThucAsync(int phienKhamId, string chanDoanCuoi)
	{
		var pk = await _repo.GetByIdAsync(phienKhamId);
		if (pk == null)
			return ApiResponse<bool>.Fail("Phiên khám không tồn tại");
		if (!string.IsNullOrEmpty(pk.ChanDoanCuoi))
			return ApiResponse<bool>.Fail("Phiên khám đã được kết thúc trước đó");
		var pkBenh = await _pkBenhrepo.GetByPhienKhamAsync(phienKhamId);
		if (pkBenh == null || !pkBenh.Any())
			return ApiResponse<bool>.Fail("Phải có ít nhất một chẩn đoán bệnh trước khi kết thúc");
		var pkCls = await _pkClsRepo.GetByPhienKhamAsync(phienKhamId);
		if (pkCls != null &&
			pkCls.Any(c => TrangThaiCLSExtensions.ToEnum(c.TrangThai) != TrangThaiCLSEnum.HoanThanh))
		{
			return ApiResponse<bool>.Fail("Tất cả CLS phải hoàn thành trước khi kết thúc phiên khám");
		}
		var hs = await _hoSoBenhAnRepo.GetByBenhNhanIdAsync(pk.BenhNhanID);
		if (hs == null)
			return ApiResponse<bool>.Fail("Chưa có hồ sơ bệnh án");
		try
		{
			pk.KetThuc(chanDoanCuoi);
		}
		catch (InvalidOperationException ex)
		{
			return ApiResponse<bool>.Fail(ex.Message);
		}
		await _repo.KetThucAsync(pk);
		return ApiResponse<bool>.SuccessResponse(true);
	}
	public async Task<ApiResponse<PhienKhamReadModel>> GetByIdAsync(int id)
	{
		var result = await _repo.GetDetailAsync(id);
		if (result == null)
			return ApiResponse<PhienKhamReadModel>.Fail("Phiên khám không tồn tại");
		return ApiResponse<PhienKhamReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PhienKhamReadModel>> GetByCaKhamIdAsync(int caKhamId)
	{
		var result = await _repo.GetByCaKhamIdAsync(caKhamId);
		if (result == null)
			return ApiResponse<PhienKhamReadModel>.Fail("Phiên khám không tồn tại");
		return ApiResponse<PhienKhamReadModel>.SuccessResponse(result);
	}
	public async Task<ApiResponse<PagedResult<PhienKhamListReadModel>>> GetByBenhNhanAsync(
		int benhNhanId, int pageNumber, int pageSize)
	{
		var (items, totalCount) =
			await _repo.GetBenhNhanPagedAsync(benhNhanId, pageNumber, pageSize);
		return ApiResponse<PagedResult<PhienKhamListReadModel>>.SuccessResponse(
			new PagedResult<PhienKhamListReadModel>
			{
				Items = items,
				TotalCount = totalCount,
				PageNumber = pageNumber,
				PageSize = pageSize
			});
	}
	public async Task<ApiResponse<PagedResult<PhienKhamListReadModel>>> GetPagedAsync(
		int pageNumber,	int pageSize, int? nhanVienID, string? trangThai)
	{
		var (items, totalCount) =
			await _repo.GetPagedAsync(pageNumber, pageSize, nhanVienID, trangThai);
		return ApiResponse<PagedResult<PhienKhamListReadModel>>.SuccessResponse(
			new PagedResult<PhienKhamListReadModel>
			{
				Items = items,
				TotalCount = totalCount,
				PageNumber = pageNumber,
				PageSize = pageSize
			});
	}
	public async Task<ApiResponse<PagedResult<PhienKhamListReadModel>>> SearchAsync(
		string keyword, int pageNumber, int pageSize, int? nhanVienID)
	{
		var (items, totalCount) =
			await _repo.SearchPagedAsync(keyword, pageNumber, pageSize, nhanVienID);
		return ApiResponse<PagedResult<PhienKhamListReadModel>>.SuccessResponse(
			new PagedResult<PhienKhamListReadModel>
			{
				Items = items,
				TotalCount = totalCount,
				PageNumber = pageNumber,
				PageSize = pageSize
			});
	}
}