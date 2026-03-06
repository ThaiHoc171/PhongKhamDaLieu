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
	private readonly IBenhNhanRepository _benhNhanRepo;
	private readonly INhanVienRepository _nhanVienRepo;
	public PhienKhamService(IPhienKhamRepository repo, IPhienKhamBenhRepository pkBenhrepo, IPhienKhamCLSRepository pkClsRepo,
		IBenhNhanRepository benhNhanRepo, INhanVienRepository nhanVienRepo, ICaKhamRepository caKhamRepo)
	{
		_repo = repo;
		_pkBenhrepo = pkBenhrepo;
		_pkClsRepo = pkClsRepo;
		_benhNhanRepo = benhNhanRepo;
		_nhanVienRepo = nhanVienRepo;
		_caKhamRepo = caKhamRepo;
	}
	public async Task<int> TaoMoiAsync(PhienKhamCreateDTO dto)
	{
		// Kiểm tra CaKham tồn tại
		var caKham = await _caKhamRepo.GetByIdAsync(dto.CaKhamID);
		if (caKham == null)
		{
			throw new Exception("Ca khám không tồn tại!");
		}
		if (caKham.TrangThai != "Đã xác nhận")
		{
			throw new Exception("Ca khám chưa được xác nhận hoặc đã kết thúc!");
		}
		// Tạo phiên khám mới
		var entity = new PhienKham(
			dto.CaKhamID,
			dto.BenhNhanID,
			dto.NhanVienID,
			dto.PhongChucNangID,
			dto.TrieuChung,
			dto.GhiChu,
			dto.HinhAnhJSON);
		return await _repo.AddAsync(entity);
	}
	public async Task CapNhatAsync(int id, PhienKhamUpdateDTO dto)
	{
		var pk = await _repo.GetByIdAsync(id)
			?? throw new Exception("Phiên khám không tồn tại");
		pk.CapNhat(
			dto.TrieuChung,
			dto.GhiChu,
			dto.PhongChucNangID,
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
		var pkBenh = await _pkBenhrepo.GetByIdAsync(phienKhamId);
		if (pkBenh == null || !pkBenh.Any())
		{
			throw new Exception("Phải có ít nhất một chẩn đoán bệnh trước khi kết thúc phiên khám");
		}
		// Kiểm tra CLS đã hoàn thành chưa
		var pkCls = await _pkClsRepo.GetByPhienKhamAsync(phienKhamId);
		if (pkCls != null && pkCls.Any(c => c.TrangThai != TrangThaiCLSEnum.HoanThanh))
		{
			throw new Exception("Tất cả các chỉ định cận lâm sàng phải được hoàn thành trước khi kết thúc phiên khám");
		}
		pk.KetThuc(chanDoanCuoi);
		// Lưu trạng thái mới
		await _repo.KetThucAsync(pk);
	}
	public async Task<PhienKhamResponseDTO> GetByIdAsync(int id)
	{
		var pk = await _repo.GetByIdAsync(id)
			?? throw new Exception("Phiên khám không tồn tại");
		var benhNhan = await _benhNhanRepo.GetNameByIdAsync(pk.BenhNhanID);
		var nhanVien = await _nhanVienRepo.GetNameByIdAsync(pk.NhanVienID);
		return new PhienKhamResponseDTO
		{
			PhienKhamID = pk.PhienKhamID,
			CaKhamID = pk.CaKhamID,
			BenhNhan = new NameResponseDTO
			{
				Id = pk.BenhNhanID,
				Name = benhNhan
			},
			NhanVien = new NameResponseDTO
			{
				Id = pk.NhanVienID,
				Name = nhanVien
			},
			PhongChucNangID = pk.PhongChucNangID,
			TrieuChung = pk.TrieuChung,
			GhiChu = pk.GhiChu,
			HinhAnhJSON = pk.HinhAnhJSON,
			ChanDoanCuoi = pk.ChanDoanCuoi,
			NgayKham = pk.NgayKham,
			TrangThai = pk.TrangThai.ToString()
		};
	}
	public async Task<PagedResult<PhienKhamResponseLiteDTO>> GetByBenhNhanAsync(int benhNhanId, int pageNumber, int pageSize)
	{
		var (data, totalCount) = await _repo.GetByBenhNhanPagedAsync(benhNhanId, pageNumber, pageSize);
		var result = new List<PhienKhamResponseLiteDTO>();
		foreach (var pk in data)
		{
			result.Add(await MapToLiteDtoAsync(pk));
		}
		return new PagedResult<PhienKhamResponseLiteDTO>
		{
			Items = result,
			TotalCount = totalCount,
			PageNumber = pageNumber,
			PageSize = pageSize
		};
	}
	public async Task<PagedResult<PhienKhamResponseLiteDTO>> GetPagedAsync(int pageNumber,int pageSize,int? nhanVienID,string? trangThai)
	{
		var (data, totalCount) = await _repo.GetPagedAsync(pageNumber, pageSize, nhanVienID, trangThai);

		var items = new List<PhienKhamResponseLiteDTO>();

		foreach (var pk in data)
		{
			items.Add(await MapToLiteDtoAsync(pk));
		}

		return new PagedResult<PhienKhamResponseLiteDTO>
		{
			Items = items,
			TotalCount = totalCount,
			PageNumber = pageNumber,
			PageSize = pageSize
		};
	}
	public async Task<List<PhienKhamResponseLiteDTO>> SearchAsync(string keyword, int? nhanVienID)
	{
		var list = await _repo.SearchAsync(keyword, nhanVienID);

		var result = new List<PhienKhamResponseLiteDTO>();

		foreach (var pk in list)
		{
			result.Add(await MapToLiteDtoAsync(pk));
		}

		return result;
	}
	private async Task<PhienKhamResponseLiteDTO> MapToLiteDtoAsync(PhienKham pk)
	{
		var benhNhan = await _benhNhanRepo.GetNameByIdAsync(pk.BenhNhanID);
		var nhanVien = await _nhanVienRepo.GetNameByIdAsync(pk.NhanVienID);
		return new PhienKhamResponseLiteDTO
		{
			PhienKhamID = pk.PhienKhamID,
			CaKhamID = pk.CaKhamID,
			BenhNhan = new NameResponseDTO{
				Id = pk.BenhNhanID,
				Name = benhNhan
			},
			NhanVien = new NameResponseDTO{
				Id = pk.NhanVienID,
				Name = nhanVien
			},
			NgayKham = pk.NgayKham,
			TrangThai = pk.TrangThai.ToString(),
			ChanDoanCuoi = pk.ChanDoanCuoi
		};
	}
}
