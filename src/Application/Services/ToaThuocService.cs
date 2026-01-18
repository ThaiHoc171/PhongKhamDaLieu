using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

public class ToaThuocService
{
	private readonly IToaThuocRepository _toaThuocRepo;
	private readonly IChiTietToaThuocRepository _chiTietRepo;

	public ToaThuocService(
		IToaThuocRepository toaThuocRepo,
		IChiTietToaThuocRepository chiTietRepo)
	{
		_toaThuocRepo = toaThuocRepo;
		_chiTietRepo = chiTietRepo;
	}

	public async Task<int> TaoToaThuocAsync(TaoToaThuocDTO dto)
	{
		var toaThuoc = new ToaThuoc(
			dto.PhienKhamID,
			dto.NhanVienKeDonID,
			dto.GhiChu);

		var toaThuocID = await _toaThuocRepo.AddAsync(toaThuoc);

		var chiTiet = dto.Thuoc
			.Select(x => new ChiTietToaThuoc(
				x.ThuocID,
				x.LieuDung,
				x.SoLuong))
			.ToList();

		await _chiTietRepo.AddAsync(toaThuocID, chiTiet);

		return toaThuocID;
	}

	public async Task<ToaThuocChiTietDTO?> XemTheoPhienKhamAsync(int phienKhamID)
	{
		var toaThuoc = await _toaThuocRepo.GetByPhienKhamIdAsync(phienKhamID);
		if (toaThuoc == null) return null;

		var chiTiet = await _chiTietRepo.GetByToaThuocIdAsync(toaThuoc.ToaThuocID);

		return new ToaThuocChiTietDTO
		{
			ToaThuocID = toaThuoc.ToaThuocID,
			NgayLap = toaThuoc.NgayLap,
			GhiChu = toaThuoc.GhiChu,
			Thuoc = chiTiet.Select(x => new ChiTietToaThuocDTO
			{
				ThuocID = x.ThuocID,
				TenThuoc = x.TenThuoc,
				LieuDung = x.LieuDung,
				SoLuong = x.SoLuong
			}).ToList()
		};
	}
}
