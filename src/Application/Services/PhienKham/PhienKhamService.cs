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
	private readonly INhanVienRepository _nhanVienRepo;
	public PhienKhamService(IPhienKhamRepository repo, IPhienKhamBenhRepository pkBenhrepo, IPhienKhamCLSRepository pkClsRepo,
		IBenhNhanRepository benhNhanRepo, INhanVienRepository nhanVienRepo, ICaKhamRepository caKhamRepo, ILichLamViecRepository lichRepo)
	{
		_repo = repo;
		_pkBenhrepo = pkBenhrepo;
		_pkClsRepo = pkClsRepo;
		_benhNhanRepo = benhNhanRepo;
		_nhanVienRepo = nhanVienRepo;
		_caKhamRepo = caKhamRepo;
		_lichRepo = lichRepo;
	}
	public async Task<int> TaoMoiAsync(int caKhamID)
	{
		// Kiểm tra CaKham tồn tại
		var caKham = await _caKhamRepo.GetByIdAsync(caKhamID);
		if (caKham == null)
			throw new Exception("Ca khám không tồn tại!");
		if (caKham.TrangThai != "Đã xác nhận")
			throw new Exception("Ca khám chưa được xác nhận hoặc đã kết thúc!");
		
		if (caKham?.ThongTinID == null)
			throw new Exception("Không có ThongTinID");
		var nv = await _lichRepo.GetNhanVienById(caKham.LichLamViecID);
		if (nv.nhanvien == 0)
			throw new Exception("Không tìm thấy nhân viên cho ca khám");
		if (nv.phong == 0)
			throw new Exception("Không tìm thấy phòng chức năng");
		var bn = await _benhNhanRepo.GetIdByThongTinAsync(caKham.ThongTinID.Value);
		if (bn == null)
			throw new Exception("Bệnh nhân không tồn tại");
		// Tạo phiên khám mới
		var entity = new PhienKham(
			caKhamID,
			bn,
			nv.nhanvien,
			nv.phong);
		return await _repo.AddAsync(entity);
	}
	public async Task CapNhatAsync(int id, PhienKhamUpdateDTO dto)
	{
		var pk = await _repo.GetByIdAsync(id)
			?? throw new Exception("Phiên khám không tồn tại");
		pk.CapNhat(
			dto.TrieuChung,
			dto.GhiChu,
			dto.HinhAnhJSON);
		await _repo.UpdateAsync(pk);
	}
	public async Task KetThucAsync(int phienKhamId, string chanDoanCuoi)
	{
		var pk = await _repo.GetByIdAsync(phienKhamId)
			?? throw new Exception("Phiên khám không tồn tại");
		// Kiểm tra đã có chẩn đoán chính chưa
		if (!string.IsNullOrEmpty(pk.ChanDoanCuoi))
		{
			throw new Exception("Phiên khám đã được kết thúc trước đó");
		}
		// Kiểm tra đã có phiếu khám bệnh chưa
		var pkBenh = await _pkBenhrepo.GetByPhienKhamAsync(phienKhamId);
		if (pkBenh == null || !pkBenh.Any())
		{
			throw new Exception("Phải có ít nhất một chẩn đoán bệnh trước khi kết thúc phiên khám");
		}
		// Kiểm tra CLS đã hoàn thành chưa
		var pkCls = await _pkClsRepo.GetByPhienKhamAsync(phienKhamId);
		if (pkCls != null && pkCls.Any(c => TrangThaiCLSExtensions.ToEnum(c.TrangThai) != TrangThaiCLSEnum.HoanThanh))
		{
			throw new Exception("Tất cả các chỉ định cận lâm sàng phải được hoàn thành trước khi kết thúc phiên khám");
		}
		pk.KetThuc(chanDoanCuoi);
		// Lưu trạng thái mới
		await _repo.KetThucAsync(pk);
	}
	public async Task<PhienKhamReadModel> GetByIdAsync(int id)
	{
		var result = await _repo.GetDetailAsync(id);

		if (result == null)
			throw new Exception("Phiên khám không tồn tại");

		return result;
	}
	public async Task<PagedResult<PhienKhamListReadModel>>GetByBenhNhanAsync(int benhNhanId, int pageNumber, int pageSize)
	{
		var (items, totalCount) =
			await _repo.GetByBenhNhanPagedAsync(benhNhanId, pageNumber, pageSize);

		return new PagedResult<PhienKhamListReadModel>
		{
			Items = items,
			TotalCount = totalCount,
			PageNumber = pageNumber,
			PageSize = pageSize
		};
	}
	public async Task<PagedResult<PhienKhamListReadModel>> GetPagedAsync(int pageNumber, int pageSize, int? nhanVienID, string? trangThai)
	{
		{
			var (items, totalCount) =
				await _repo.GetPagedAsync(pageNumber, pageSize, nhanVienID, trangThai);

			return new PagedResult<PhienKhamListReadModel>
			{
				Items = items,
				TotalCount = totalCount,
				PageNumber = pageNumber,
				PageSize = pageSize
			};
		}
	}
	public async Task<List<PhienKhamListReadModel>> SearchAsync(string keyword, int? nhanVienID)
		=> await _repo.SearchAsync(keyword, nhanVienID);
}
