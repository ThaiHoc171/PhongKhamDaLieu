using Application.DTOs;
using Application.Interfaces;
using Application.ReadModels;
using Domain.Entities;

namespace Application.Services;

public class PhienKhamService
{
	private readonly IPhienKhamRepository _repo;
	private readonly ICaKhamRepository _caKhamRepo;
	private readonly IPhienKhamBenhRepository _pkBenhrepo;
	private readonly IPhienKhamCLSRepository _pkClsRepo;

	public PhienKhamService(IPhienKhamRepository repo, IPhienKhamBenhRepository pkBenhrepo, IPhienKhamCLSRepository pkClsRepo)
	{
		_repo = repo;
		_pkBenhrepo = pkBenhrepo;
		_pkClsRepo = pkClsRepo;
	}

	public async Task<int> TaoMoiAsync(PhienKhamCreateDTO dto)
	{
		// Kiểm tra CaKham tồn tại
		var caKham = await _caKhamRepo.GetByIdAsync(dto.CaKhamID);
		if (caKham == null)
		{
			throw new Exception("Ca khám không tồn tại!");
		}
		if (caKham.TrangThai != "Đang khám")
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
	public async Task KetThucAsync(int phienKhamId, string chuanDoanCuoi)
	{
		var pk = await _repo.GetByIdAsync(phienKhamId)
			?? throw new Exception("Phiên khám không tồn tại");
		// Kiểm tra đã có chẩn đoán chính chưa
		if (!string.IsNullOrEmpty(pk.ChuanDoanCuoi))
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
		if (pkCls != null && pkCls.Any(c => c.TrangThai != "Hoàn thành"))
		{
			throw new Exception("Tất cả các chỉ định cận lâm sàng phải được hoàn thành trước khi kết thúc phiên khám");
		}


		// Nghiệp vụ nằm trong Entity
		pk.KetThuc(chuanDoanCuoi);

		// Lưu trạng thái mới
		await _repo.KetThucAsync(pk);
	}
	public async Task<PhienKhamReadModel?> LayTheoIdAsync(int id)
	{
		return await _repo.GetReadModelByIdAsync(id);
	}

	public async Task<List<PhienKhamReadModel>> LayTatCaAsync()
	{
		return await _repo.GetAllAsync();
	}

	public async Task<List<PhienKhamReadModel>> LocAsync(DateTime? tuNgay,DateTime? denNgay,string? trangThai,int? nhanVienID)
	{
		return await _repo.FilterAsync(tuNgay, denNgay, trangThai, nhanVienID);
	}
}
